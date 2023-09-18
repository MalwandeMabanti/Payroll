namespace Payroll.Services
{
    using System.Collections.Generic;

    using Payroll.Data;
    using Payroll.Interfaces;
    using Payroll.Models;
    using Payroll.ViewModels;

    public class EmployeeQualificationService : GenericService<EmployeeQualification>, IEmployeeQualificationService
    {
        private readonly ApplicationDbContext context;
        private readonly IEnumEducationLevelService enumEducationLevelService;
        private readonly IEnumInstitutionTypeService enumInstitutionTypeService;
        private readonly IEnumQualificationService enumQualificationService;

        public EmployeeQualificationService(
            ApplicationDbContext context,
            IEnumEducationLevelService enumEducationLevelService,
            IEnumInstitutionTypeService enumInstitutionTypeService,
            IEnumQualificationService enumQualificationService)
            : base(context)
        {
            this.context = context;
            this.enumEducationLevelService = enumEducationLevelService;
            this.enumInstitutionTypeService = enumInstitutionTypeService;
            this.enumQualificationService = enumQualificationService;
        }

        public EmployeeQualificationsViewModel BuildIndexViewModel()
        {
            EmployeeQualificationsViewModel employeeQualificationsViewModel = new ();

            employeeQualificationsViewModel.EducationLevelList = this.enumEducationLevelService.GetAll();
            employeeQualificationsViewModel.InstitutionTypeList = this.enumInstitutionTypeService.GetAll();
            employeeQualificationsViewModel.QualificationList = this.enumQualificationService.GetAll();

            List<EmployeeQualification> employeeQualifications = new List<EmployeeQualification>();

            foreach (var qualification in this.context.EmployeeQualifications)
            {
                employeeQualifications.Add(new EmployeeQualification()
                {
                    EmployeeQualificationID = qualification.EmployeeQualificationID,
                    EmpId = qualification.EmpId,
                    Institution = qualification.Institution,
                    InProgress = qualification.InProgress,
                    DateCompleted = qualification.DateCompleted,
                    QualificationID = employeeQualificationsViewModel.QualificationList.FirstOrDefault(_ => _.QualificationsID == qualification.QualificationID) !.QualificationsID,
                    EducationLevelID = employeeQualificationsViewModel.EducationLevelList.FirstOrDefault(_ => _.EducationLevelID == qualification.EducationLevelID) !.EducationLevelID,
                    InstitutionType = employeeQualificationsViewModel.InstitutionTypeList.FirstOrDefault(_ => _.InstitutionTypeID == qualification.InstitutionType) !.InstitutionTypeID,
                });
            }

            employeeQualificationsViewModel.QualificationModelList = employeeQualifications;
            return employeeQualificationsViewModel;
        }

        public EmployeeQualificationsViewModel BuildCreateViewModel()
        {
            return new EmployeeQualificationsViewModel()
            {
                EducationLevelList = this.enumEducationLevelService.GetAll(),
                InstitutionTypeList = this.enumInstitutionTypeService.GetAll(),
                QualificationList = this.enumQualificationService.GetAll(),
            };
        }

        public EmployeeQualificationsViewModel BuildEditViewModel(int id)
        {
            EmployeeQualification employeeQualification = this.context.EmployeeQualifications.FirstOrDefault(_ => _.EmployeeQualificationID == id) !;

            var enums = new EnumsViewModel()
            {
                EducationLevel = this.enumEducationLevelService.GetAll(),
                InstitutionType = this.enumInstitutionTypeService.GetAll(),
                Qualification = this.enumQualificationService.GetAll(),
            };

            EmployeeQualificationsViewModel editViewModel = new ()
            {
                QualificationModel = new EmployeeQualification()
                {
                    EmployeeQualificationID = employeeQualification.EmployeeQualificationID,
                    EmpId = employeeQualification.EmpId,
                    Institution = employeeQualification.Institution,
                    InProgress = employeeQualification.InProgress,
                    DateCompleted = employeeQualification.DateCompleted,

                    EducationLevelID = enums.EducationLevel.FirstOrDefault(_ => _.EducationLevelID == employeeQualification.EducationLevelID) !.EducationLevelID,
                    InstitutionType = enums.InstitutionType.FirstOrDefault(_ => _.InstitutionTypeID == employeeQualification.InstitutionType) !.InstitutionTypeID,
                    QualificationID = enums.Qualification.FirstOrDefault(_ => _.QualificationsID == employeeQualification.QualificationID) !.QualificationsID,
                },

                EducationLevelList = enums.EducationLevel,
                InstitutionTypeList = enums.InstitutionType,
                QualificationList = enums.Qualification,
            };
            return editViewModel;
        }
    }
}
