using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OrganisationStructureApi.Dtos;
using OrganisationStructureApi.Services;

namespace OrganisationStructureApi.Controllers
{
    [ApiController]
    [Route("api/employees")]
    public class EmployeesController(IEmployeeService employeeService) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeResponse>>> GetEmployees()
        {
            return Ok(await employeeService.GetAllEmployeesAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeResponse>> GetEmployee(int id)
        {
            return Ok(await employeeService.GetEmployeeByIdAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeResponse>> CreateEmployee(EmployeeRequest employee)
        {
            var created = await employeeService.AddEmployeeAsync(employee);
            return CreatedAtAction(nameof(GetEmployee), new { id = created.Id }, created);
        }
    
        [HttpPut("{id}")]
        public async Task<ActionResult<EmployeeResponse>> UpdateEmployee(int id, EmployeeRequest employee)
        {   
            return Ok(await employeeService.UpdateEmployeeAsync(id, employee));     
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            await employeeService.DeleteEmployeeAsync(id);
            return NoContent();
        }
    }
}
