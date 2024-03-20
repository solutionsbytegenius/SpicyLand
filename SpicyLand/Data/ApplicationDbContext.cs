using Microsoft.EntityFrameworkCore;
using SpicyLand.Models;

namespace SpicyLand.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() 
        {
        }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<UserEntity> User { get; set; }
        public DbSet<PaninoEntity> Panino { get; set; }
        public DbSet<NewsEntity> News { get; set; }
        public DbSet<OrdineEntity> Ordine { get; set; }
        public DbSet<OrarioEntity> Orario { get; set; }
    }
}
