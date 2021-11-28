using FlasherWebApi.DTO;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FlasherWebApi.Models
{
    public class DatabaseContext : IdentityDbContext<User,Role,int>
    {

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Subscriptor> Subscriptors { get; set; }
        
    }
}
