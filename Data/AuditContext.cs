namespace Payroll.Data
{
    using Microsoft.EntityFrameworkCore;

    using Payroll.Models;

    public class AuditContext : DbContext
    {
        public AuditContext(DbContextOptions<AuditContext> options)
            : base(options)
        {
        }

        public DbSet<Audit> AuditTrail { get; set; }

        public DbSet<AuditTable> AuditTable { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("audit");

            base.OnModelCreating(modelBuilder);
        }
    }
}
