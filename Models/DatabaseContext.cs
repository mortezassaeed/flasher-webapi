using Microsoft.EntityFrameworkCore;

namespace FlasherWebApi.Models
{
    public class DatabaseContext : DbContext
    {

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //}

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //}
        //entities

        public DbSet<PushSubscription> PushSubscription { get; set; }
    }
}
