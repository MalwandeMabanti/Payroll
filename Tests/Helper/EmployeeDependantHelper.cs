namespace Payroll.Tests.Helper
{
    using Payroll.Models;

    public static class EmployeeDependantHelper
    {
        public static EmployeeDependant CreateEmployeeDependant()
        {
            EmployeeDependant employeeDependant = new ()
            {
                EmployeeDependantID = 1,
                EmployeeId = 2,
                EffectiveDate = DateTime.Now,
                FirstName = "Test",
                LastName = "Test",
                ContactNumber = 123,
                MedicalAidDependant = true,
                BirthDate = DateTime.Now,
                DependantTypeID = 3,
            };

            return employeeDependant;
        }
    }
}
