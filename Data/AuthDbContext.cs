﻿namespace Payroll.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class AuthDbContext : IdentityDbContext<PayrollUser>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
        }
    }
}
