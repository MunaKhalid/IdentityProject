using IdentityFrame.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace IdentityFrame.data
{
    public class IdentityContext : IdentityDbContext< ApplicaionUser >
    {
        private readonly IConfiguration configuration;
        public IdentityContext(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        public DbSet<Employee> employees { get; set; }
       

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // optionsBuilder.UseSqlServer("data source=localhost; initial catalog=identity; integrated security=true");
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));
            base.OnConfiguring(optionsBuilder);
        }

    }
}
