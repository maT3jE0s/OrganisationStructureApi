using Microsoft.EntityFrameworkCore;
using OrganisationStructureApi.Data;
using OrganisationStructureApi.Dtos;
using OrganisationStructureApi.Exceptions;
using OrganisationStructureApi.Models;

namespace OrganisationStructureApi.Services
{
    public class OrgNodeService(AppDbContext db) : IOrgNodeService
    {
        public async Task<OrgNodeResponse> AddOrgNodeAsync(OrgNodeType type, OrgNodeRequest orgNode)
        {
            var leader = await ValidateAsync(type, orgNode, null);

            var newOrgNode = OrgNode.fromDto(orgNode);
            newOrgNode.Type = type;
            db.OrgNodes.Add(newOrgNode);
            await db.SaveChangesAsync();

            if (type == OrgNodeType.Company)
            {
                leader.CompanyId = newOrgNode.Id;
                await db.SaveChangesAsync();
            }

            return newOrgNode.toDto();
        }

        public async Task DeleteOrgNodeAsync(OrgNodeType type, int id)
        {
            var orgNode = await FindByIdAsync(type, id);

            List<OrgNode> childNodes = await db.OrgNodes.Where(n => n.ParentId == id).ToListAsync();

            foreach (var childNode in childNodes)
            {
                await DeleteOrgNodeAsync(childNode.Type, childNode.Id);
            }

            db.OrgNodes.Remove(orgNode);
            await db.SaveChangesAsync();
        }

        public async Task<List<OrgNodeResponse>> GetAllOrgNodesAsync(OrgNodeType type)
        {
            var orgNodes = await db.OrgNodes.Where(n => n.Type == type).ToListAsync();
            return orgNodes.Select(n => n.toDto()).ToList();
        }

        public async Task<OrgNodeResponse> GetOrgNodeByIdAsync(OrgNodeType type, int id)
        {
            var orgNode = await FindByIdAsync(type, id);
            return orgNode.toDto();
        }

        public async Task<OrgNodeResponse> UpdateOrgNodeAsync(OrgNodeType type, int id, OrgNodeRequest orgNode)
        {
            var updateOrgNode = await FindByIdAsync(type, id);

            var leader = await ValidateAsync(type, orgNode, updateOrgNode);

            if (updateOrgNode.ParentId != orgNode.ParentId
                && await GetCompanyIdAsync(updateOrgNode) != await GetCompanyIdAsync(OrgNode.fromDto(orgNode)))
            {
                throw new InvalidOperationException("Node cannot be moved to a different company.");
            }

            updateOrgNode.Name = orgNode.Name;
            updateOrgNode.Code = orgNode.Code;
            updateOrgNode.ParentId = orgNode.ParentId;
            updateOrgNode.LeaderId = orgNode.LeaderId;

            if (type == OrgNodeType.Company) leader.CompanyId = updateOrgNode.Id;

            await db.SaveChangesAsync();
            return updateOrgNode.toDto();
        }

        private async Task<OrgNode> FindByIdAsync(OrgNodeType type, int id) 
        {
            return await db.OrgNodes.FirstOrDefaultAsync(n => n.Id == id && n.Type == type)
                ?? throw new ResourceNotFoundException($"{type} with ID {id} not found");
        }

        private async Task<int> GetCompanyIdAsync(OrgNode node)
        {
            while (node.Type != OrgNodeType.Company)
            {
                node = await db.OrgNodes.FirstAsync(n => n.Id == node.ParentId.Value);
            }
            return node.Id;
        }

        private async Task<Employee> ValidateAsync(OrgNodeType type, OrgNodeRequest orgNode, OrgNode? existing)
        {
            int? companyId;

            if (type == OrgNodeType.Company)
            {
                if (orgNode.ParentId.HasValue)
                {
                    throw new InvalidOperationException("Company cannot have a parent");
                }
                companyId = existing?.Id;
            }
            else
            {
                if (!orgNode.ParentId.HasValue)
                {
                    throw new InvalidOperationException($"{type} must have a parent");
                }

                var parent = await db.OrgNodes.FirstOrDefaultAsync(n => n.Id == orgNode.ParentId.Value && n.Type == type - 1)
                    ?? throw new InvalidOperationException($"Parent with ID {orgNode.ParentId.Value} and type {type - 1} not found");

                companyId = await GetCompanyIdAsync(parent);
            }

            var leader = await db.Employees.FirstOrDefaultAsync(e => e.Id == orgNode.LeaderId);
            if (leader == null)
            {
                throw new InvalidOperationException($"Leader with ID {orgNode.LeaderId} not found");
            }

            if (type == OrgNodeType.Company)
            {
                if (leader.CompanyId is not null && leader.CompanyId != companyId)
                    throw new InvalidOperationException("Leader already belongs to a different company.");
            }
            else if (leader.CompanyId != companyId)
            {
                throw new InvalidOperationException("Leader must be an employee of the same company.");
            }

            return leader;
        }
    }
}
