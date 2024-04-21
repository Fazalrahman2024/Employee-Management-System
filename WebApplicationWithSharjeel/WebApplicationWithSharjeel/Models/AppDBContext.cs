using Microsoft.EntityFrameworkCore;
using SharedEntityClasses.MainClasses;

namespace WebApplicationWithSharjeel.Models
{
    public class AppDBContext : DbContext
    {
        // Constructor
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


        }

    }
}
