namespace Payroll.Interfaces
{
    using Payroll.Models;

    public interface IEmployeeService : IGenericService<Employee>
    {
        bool EmployeeExists(string empNumber);

        Employee GetLastEmployee();

        string GenerateEmployeeNumber();
    }
}
