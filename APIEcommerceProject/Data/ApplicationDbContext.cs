using APIEcommerceProject.Models;
using Microsoft.EntityFrameworkCore;

namespace APIEcommerceProject.Data
{
    public class ApplicationDbContext:DbContext
    {
        public DbSet<Category> Categories { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=.;DataBase=ApiEcommerceProject;TrustServerCertificate=True;Trusted_Connection=True");


        }

    }
}
