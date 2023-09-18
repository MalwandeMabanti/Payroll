namespace Payroll.Services
{
    using Payroll.Data;
    using Payroll.Interfaces;
    using Payroll.Models;

    public class EmployeeAddressService : GenericService<EmployeeAddress>, IEmployeeAddressService
    {
        public EmployeeAddressService(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
