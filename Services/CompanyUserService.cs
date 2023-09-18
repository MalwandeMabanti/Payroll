namespace Payroll.Services
{
    using Payroll.Data;
    using Payroll.Interfaces;
    using Payroll.Models;

    public class CompanyUserService : GenericService<CompanyUser>, ICompanyUserService
    {
        private readonly ApplicationDbContext context;

        public CompanyUserService(ApplicationDbContext context)
            : base(context)
        {
            this.context = context;
        }

        public CompanyUser GetCompanyUser(string companyUserEmail)
        {
            return this.context.CompanyUsers.FirstOrDefault(_ => _.Email == companyUserEmail);
        }
    }
}
