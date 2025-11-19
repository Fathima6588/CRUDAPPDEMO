using CRUDAPPDEMO.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDAPPDEMO.DATA
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options): base(options) 
        {
            
        }

        public DbSet<Student> Students { get; set; }
    }
}
