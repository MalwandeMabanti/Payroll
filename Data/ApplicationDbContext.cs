namespace Payroll.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Diagnostics;

    using Payroll.Models;

    public class ApplicationDbContext : DbContext
    {
        private readonly ISaveChangesInterceptor auditMiddleware;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ISaveChangesInterceptor auditMiddleware)
            : base(options)
        {
            this.auditMiddleware = auditMiddleware;
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<EmployeeDependant> EmployeeDependants { get; set; }

        public DbSet<EnumDependantType> EnumDependantTypes { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<EnumTitle> EnumTitles { get; set; }

        public DbSet<EmployeeAddress> EmployeeAddresses { get; set; }

        public DbSet<EnumCountry> EnumCountries { get; set; }

        public DbSet<EmployeeQualification> EmployeeQualifications { get; set; }

        public DbSet<EnumQualification> EnumQualifications { get; set; }

        public DbSet<EnumEducationLevel> EnumEducationLevels { get; set; }

        public DbSet<EnumInstitutionTypes> EnumInstitutionTypes { get; set; }

        public DbSet<CompanyUser> CompanyUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (this.auditMiddleware != null)
            {
                optionsBuilder.AddInterceptors(this.auditMiddleware);
            }
        }
    }
}
