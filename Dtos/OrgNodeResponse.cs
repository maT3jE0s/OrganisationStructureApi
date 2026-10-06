using OrganisationStructureApi.Models;

namespace OrganisationStructureApi.Dtos
{
    public class OrgNodeResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;

        public OrgNodeType Type { get; set; }

        public int? ParentId { get; set; }
        public int LeaderId { get; set; }
    }
}
