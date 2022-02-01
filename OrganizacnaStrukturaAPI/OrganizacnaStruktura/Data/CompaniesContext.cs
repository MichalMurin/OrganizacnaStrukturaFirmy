using OrganizacnaStruktura.Models;
using Microsoft.EntityFrameworkCore;

namespace OrganizacnaStruktura.Data
{
    public class CompaniesContext: DbContext
    {
        public CompaniesContext(DbContextOptions<CompaniesContext> opt) : base(opt)
        {
            
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Division> Divisions { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Department> Departments { get; set; }        
        public DbSet<Employee> Employees{get;set;}
        
    }
}