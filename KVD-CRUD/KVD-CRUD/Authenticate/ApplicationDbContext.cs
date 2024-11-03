using KVD_CRUD.Models;
using Microsoft.EntityFrameworkCore;

namespace KVD_CRUD.Authenticate
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
