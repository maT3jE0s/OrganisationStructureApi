using System.ComponentModel.DataAnnotations;

namespace OrganisationStructureApi.Dtos
{
    public class EmployeeRequest
    {
        [Required(ErrorMessage = "Degree is required")]
        [StringLength(10, ErrorMessage = "Degree must be at most 10 characters long")]
        [RegularExpression(@"^[a-zA-Z\s\-\.]+$", ErrorMessage = "Degree must contain only letters, spaces, hyphens, and dots")]
        public String Degree { get; set; } = String.Empty;

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Name must be at most 50 characters long")]
        [RegularExpression(@"^[\p{L}\s\-']+$", ErrorMessage = "Name must contain only letters, spaces, hyphens, and apostrophes")]
        public String Name { get; set; } = String.Empty;

        [Required(ErrorMessage = "Surname is required")]
        [StringLength(50, ErrorMessage = "Surname must be at most 50 characters long")]
        [RegularExpression(@"^[\p{L}\s\-']+$", ErrorMessage = "Surname must contain only letters, spaces, hyphens, and apostrophes")]
        public String Surname { get; set; } = String.Empty;

        [Required(ErrorMessage = "Phone is required")]
        [StringLength(15, ErrorMessage = "Phone must be at most 15 characters long")]
        [RegularExpression(@"^\+?[0-9\s\-()]+$", ErrorMessage = "Phone must be a valid phone number")]
        public String Phone { get; set; } = String.Empty;

        [Required(ErrorMessage = "Email is required")]
        [StringLength(50, ErrorMessage = "Email must be at most 50 characters long")]
        [EmailAddress(ErrorMessage = "Email must be a valid email address")]
        public String Email { get; set; } = String.Empty;
    }
}
