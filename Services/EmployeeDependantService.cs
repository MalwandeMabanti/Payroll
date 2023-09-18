namespace Payroll.Services
{
    using Payroll.Data;
    using Payroll.Interfaces;
    using Payroll.Models;

    public class EmployeeDependantService : GenericService<EmployeeDependant>, IEmployeeDependantService
    {
        public EmployeeDependantService(ApplicationDbContext context)

            : base(context)
        {
        }
    }
}
