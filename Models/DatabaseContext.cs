using FlasherWebApi.DTO;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FlasherWebApi.Models
{
    public class DatabaseContext : DbContext
    {

        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Map table names
        }

        public DbSet<Subscriptor> Subscriptors { get; set; }
        
    }
}
