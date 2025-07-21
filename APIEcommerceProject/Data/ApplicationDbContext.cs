using APIEcommerceProject.Models.Category;
using Microsoft.EntityFrameworkCore;

namespace APIEcommerceProject.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryTranslation> CategoryTranslations { get; set; }
       

    }
}
