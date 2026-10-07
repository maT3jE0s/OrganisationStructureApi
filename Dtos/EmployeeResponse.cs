namespace OrganisationStructureApi.Dtos
{
    public class EmployeeResponse
    {
        public int Id { get; set; }
        public String Degree { get; set; } = String.Empty;
        public String Name { get; set; } = String.Empty;
        public String Surname { get; set; } = String.Empty;
        public String Phone { get; set; } = String.Empty;
        public String Email { get; set; } = String.Empty;
        public int? CompanyId { get; set; }
    }
}
