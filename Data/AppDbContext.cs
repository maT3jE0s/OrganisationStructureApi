using Microsoft.EntityFrameworkCore;
using OrganisationStructureApi.Models;

namespace OrganisationStructureApi.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<OrgNode> OrgNodes { get; set; } = null!;
    }
}
