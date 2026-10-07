using OrganisationStructureApi.Models;
using System.ComponentModel.DataAnnotations;

namespace OrganisationStructureApi.Dtos
{
    public class OrgNodeRequest
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Name must be at most 50 characters long")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Code is required")]
        [StringLength(50, ErrorMessage = "Code must be at most 50 characters long")]
        [RegularExpression(@"^[0-9\s\-]+$", ErrorMessage = "Code must contain only numbers, spaces, hyphens")]
        public string Code { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "ParentId must be a positive integer")]
        public int? ParentId { get; set; }

        [Required(ErrorMessage = "LeaderId is required")]
        [Range(1, int.MaxValue, ErrorMessage = "LeaderId must be a positive integer")]
        public int LeaderId { get; set; }
    }
}