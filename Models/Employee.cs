using OrganisationStructureApi.Dtos;

namespace OrganisationStructureApi.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public String Degree { get; set; } = String.Empty;
        public String Name { get; set; } = String.Empty;
        public String Surname { get; set; } = String.Empty;
        public String Phone { get; set; } = String.Empty;
        public String Email { get; set; } = String.Empty;

        public static Employee fromDto(EmployeeRequest employeeRequest)
        {
            return new Employee
            {
                Degree = employeeRequest.Degree,
                Name = employeeRequest.Name,
                Surname = employeeRequest.Surname,
                Phone = employeeRequest.Phone,
                Email = employeeRequest.Email
            };
        }

        public EmployeeResponse toDto()
        {
            return new EmployeeResponse
            {
                Id = this.Id,
                Degree = this.Degree,
                Name = this.Name,
                Surname = this.Surname,
                Phone = this.Phone,
                Email = this.Email
            };
        }
    }
}
