using OrganisationStructureApi.Dtos;

namespace OrganisationStructureApi.Models
{
    public class OrgNode
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;

        public OrgNodeType Type { get; set; }

        public int? ParentId { get; set; }
        public int LeaderId { get; set; }

        public static OrgNode fromDto(OrgNodeRequest orgNodeRequest)
        {
            return new OrgNode
            {
                Name = orgNodeRequest.Name,
                Code = orgNodeRequest.Code,
                ParentId = orgNodeRequest.ParentId,
                LeaderId = orgNodeRequest.LeaderId
            };
        }

        public OrgNodeResponse toDto()
        {
            return new OrgNodeResponse
            {
                Id = this.Id,
                Name = this.Name,
                Code = this.Code,
                Type = this.Type,
                ParentId = this.ParentId,
                LeaderId = this.LeaderId
            };
        }
    }
}
