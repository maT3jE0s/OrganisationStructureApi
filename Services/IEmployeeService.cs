using OrganisationStructureApi.Models;
using OrganisationStructureApi.Dtos;

namespace OrganisationStructureApi.Services
{
    public interface IEmployeeService
    {
        Task<List<EmployeeResponse>> GetAllEmployeesAsync();
        Task<EmployeeResponse> GetEmployeeByIdAsync(int id);
        Task<EmployeeResponse> AddEmployeeAsync(EmployeeRequest employee);
        Task<EmployeeResponse> UpdateEmployeeAsync(int id, EmployeeRequest employee);
        Task DeleteEmployeeAsync(int id);
    }
}
