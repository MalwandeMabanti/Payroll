namespace Payroll.Services
{
    using Payroll.Data;
    using Payroll.Interfaces;
    using Payroll.Models;

    public class EmployeeService : GenericService<Employee>, IEmployeeService
    {
        private readonly ApplicationDbContext context;

        public EmployeeService(ApplicationDbContext context)
            : base(context)
        {
            this.context = context;
        }

        public bool EmployeeExists(string empNumber)
        {
            return this.context.Employees.Any(_ => _.EmployeeNumber == empNumber);
        }

        public Employee GetLastEmployee()
        {
            return this.context.Employees.OrderBy(_ => _.EmployeeId).LastOrDefault();
        }

        public string GenerateEmployeeNumber()
        {
            var prefix = "EMP";
            var lastEmployeeNumber = this.GetLastEmployee()?.EmployeeNumber;

            if (lastEmployeeNumber == null)
            {
                return prefix + "00" + 1;
            }

            var suffix = lastEmployeeNumber.Substring(lastEmployeeNumber.IndexOf(prefix) + prefix.Length);
            var numberSuffix = int.Parse(suffix) + 1;

            return prefix + numberSuffix.ToString().PadLeft(suffix.Length, '0');
        }
    }
}
