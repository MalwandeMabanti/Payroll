namespace Payroll.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<PayrollUser>
    {
        public void Configure(EntityTypeBuilder<PayrollUser> builder)
        {
            builder.Property(_ => _.FirstName).HasMaxLength(255);
            builder.Property(_ => _.LastName).HasMaxLength(255);
        }
    }
}
