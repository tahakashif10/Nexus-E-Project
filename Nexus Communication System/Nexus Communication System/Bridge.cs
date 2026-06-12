using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Nexus_Communication_System.Models;

namespace Nexus_Communication_System.Data
{
    public class Bridge : IdentityDbContext<ApplicationUser>
    {
        public Bridge(DbContextOptions<Bridge> options)
            : base(options)
        {
        }

        public DbSet<ServiceCategory> ServiceCategories { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}