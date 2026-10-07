using OrganisationStructureApi.Data;
using OrganisationStructureApi.Models;
using OrganisationStructureApi.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrganisationStructureApi.Exceptions;

namespace OrganisationStructureApi.Services
{
    public class EmployeeService(AppDbContext db) : IEmployeeService
    {
        public async Task<EmployeeResponse> AddEmployeeAsync(EmployeeRequest employee)
        {
            await ValidateCompanyExistanceAsync(employee.CompanyId);
            var newEmployee = Employee.fromDto(employee);
            db.Employees.Add(newEmployee);
            await db.SaveChangesAsync();
            return newEmployee.toDto();
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            var employee = await FindByIdAsync(id);

            if (await db.OrgNodes.AnyAsync(o => o.LeaderId == id))
            {
                throw new InvalidOperationException("Cannot delete employee who is a leader of an organization node.");
            }

            db.Employees.Remove(employee);
            await db.SaveChangesAsync();
        }

        public async Task<List<EmployeeResponse>> GetAllEmployeesAsync()
        {
            var employees = await db.Employees.ToListAsync();
            return employees.Select(e => e.toDto()).ToList();
        }

        public async Task<EmployeeResponse> GetEmployeeByIdAsync(int id)
        {
            var employee = await FindByIdAsync(id);
            return employee.toDto();
        }

        public async Task<EmployeeResponse> UpdateEmployeeAsync(int id, EmployeeRequest employee)
        {
            var updateEmployee = await FindByIdAsync(id);
            await ValidateCompanyExistanceAsync(employee.CompanyId);

            if (updateEmployee.CompanyId != employee.CompanyId
                && await db.OrgNodes.AnyAsync(o => o.LeaderId == id))
            {
                throw new InvalidOperationException("Employee leads an organization node, company cannot be changed.");
            }

            updateEmployee.Degree = employee.Degree;
            updateEmployee.Name = employee.Name;
            updateEmployee.Surname = employee.Surname;
            updateEmployee.Phone = employee.Phone;
            updateEmployee.Email = employee.Email;
            updateEmployee.CompanyId = employee.CompanyId;

            await db.SaveChangesAsync();
            return updateEmployee.toDto();
        }

        private async Task<Employee> FindByIdAsync(int id)
        {
            return await db.Employees.FirstOrDefaultAsync(e => e.Id == id)
                ?? throw new ResourceNotFoundException($"Employee with ID {id} not found");
        }

        private async Task ValidateCompanyExistanceAsync(int? companyId)
        {
            if (companyId.HasValue && !await db.OrgNodes.AnyAsync(n => n.Id == companyId.Value && n.Type == OrgNodeType.Company))
            {
                throw new InvalidOperationException($"Company with ID {companyId.Value} does not exist.");
            }
        }
    }
}
