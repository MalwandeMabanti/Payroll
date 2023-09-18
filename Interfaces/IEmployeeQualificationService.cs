namespace Payroll.Interfaces
{
    using Payroll.Models;
    using Payroll.ViewModels;

    public interface IEmployeeQualificationService : IGenericService<EmployeeQualification>
    {
        EmployeeQualificationsViewModel BuildIndexViewModel();

        EmployeeQualificationsViewModel BuildCreateViewModel();

        EmployeeQualificationsViewModel BuildEditViewModel(int id);
    }
}
