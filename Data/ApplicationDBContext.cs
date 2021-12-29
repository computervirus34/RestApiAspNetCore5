using Microsoft.EntityFrameworkCore;
using RestAPI;

namespace RestAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {

        }
    }
}