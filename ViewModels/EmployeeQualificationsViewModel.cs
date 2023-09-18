namespace Payroll.ViewModels
{
    using Payroll.Models;

    public class EmployeeQualificationsViewModel
    {
        public EmployeeQualification QualificationModel { get; set; }

        public List<EmployeeQualification> QualificationModelList { get; set; }

        public List<EnumEducationLevel> EducationLevelList { get; set; }

        public List<EnumInstitutionTypes> InstitutionTypeList { get; set; }

        public List<EnumQualification> QualificationList { get; set; }
    }
}
