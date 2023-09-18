namespace Payroll.Tests.Helpers
{
    using Payroll.Models;

    public static class EmployeeHelper
    {
        public static Employee CreateEmployee()
        {
            return new Employee
            {
                EmployeeId = 1,
                EmployeeNumber = "EMP001",
                LastName = "Smith",
                FirstName = "John",
                Email = "john@gmail.com",
                MiddleName = "A",
                Initials = "JAS",
                PreferredName = "John",
                TitleId = 1,
                EmployeeRetired = false,
                MaidenName = "Smith",
            };
        }
    }
}