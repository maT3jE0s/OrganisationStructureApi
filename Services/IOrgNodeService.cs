using OrganisationStructureApi.Dtos;
using OrganisationStructureApi.Models;

namespace OrganisationStructureApi.Services
{
    public interface IOrgNodeService
    {
        Task<List<OrgNodeResponse>> GetAllOrgNodesAsync(OrgNodeType type);
        Task<OrgNodeResponse> GetOrgNodeByIdAsync(OrgNodeType type, int id);
        Task<OrgNodeResponse> AddOrgNodeAsync(OrgNodeType type, OrgNodeRequest orgNode);
        Task<OrgNodeResponse> UpdateOrgNodeAsync(OrgNodeType type, int id, OrgNodeRequest orgNode);
        Task DeleteOrgNodeAsync(OrgNodeType type, int id);
    }
}
