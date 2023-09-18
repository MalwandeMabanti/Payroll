namespace Payroll.Tests.Helpers
{
    using Payroll.Models;

    public static class EmployeeQualificationsHelper
    {
        public static EmployeeQualification GetValidModel()
        {
            return new EmployeeQualification
            {
                EmployeeQualificationID = 1,
                EmpId = 1,
                QualificationID = 1,
                EducationLevelID = 1,
                Institution = "Valid Institution",
                InstitutionType = 1,
                InProgress = true,
                DateCompleted = DateTime.Now.AddDays(-10),
            };
        }
    }
}
