namespace Payroll.Validators
{
    using FluentValidation;

    using Payroll.Models;
    using Payroll.Validators.ErrorCodes;

    public class EmployeeQualificationValidator : AbstractValidator<EmployeeQualification>
    {
        public EmployeeQualificationValidator()
        {
            this.RuleSet("Create", () =>
            {
                this.GeneralRules();
            });

            this.RuleSet("Edit", () =>
            {
                this.RuleFor(_ => _.EmployeeQualificationID)
                    .GreaterThan(0)
                    .WithErrorCode(EmployeeQualificationsErrorCodes.EmployeeQualificationIdLessThanOne)
                    .WithMessage("Employee Qualification ID cannot be 0 or less")
                    .NotNull();

                this.GeneralRules();
            });

            this.RuleSet("Delete", () =>
            {
                this.RuleFor(_ => _.EmployeeQualificationID)
                    .GreaterThan(0)
                    .WithErrorCode(EmployeeQualificationsErrorCodes.EmployeeQualificationIdLessThanOne)
                    .WithMessage("Employee Qualification ID cannot be 0 or less");
            });
        }

        private void GeneralRules()
        {
            this.RuleFor(_ => _.EmpId)
                .GreaterThan(0)
                .WithErrorCode(EmployeeQualificationsErrorCodes.EmpIdLessThanOne)
                .WithMessage("Employee Id Cannot be less than 1")
                .NotNull()
                .WithErrorCode(EmployeeQualificationsErrorCodes.EmpIdIsNull);

            this.RuleFor(_ => _.Institution)
                .NotNull()
                .MinimumLength(1)
                .WithMessage("Insititution Cannot Be Empty")
                .MaximumLength(30)
                .WithErrorCode(EmployeeQualificationsErrorCodes.InstitutionLengthOverLimit)
                .WithMessage("Institution Cannot Be More Than 30 Characters");

            this.RuleFor(_ => _.QualificationID)
                .GreaterThan(0)
                .WithErrorCode(EmployeeQualificationsErrorCodes.QualificationIdLessThanOne)
                .WithMessage("Please Select a Qualification")
                .NotNull();

            this.RuleFor(_ => _.EducationLevelID)
                .GreaterThan(0)
                .WithErrorCode(EmployeeQualificationsErrorCodes.EducationLevelIdLessThanOne)
                .WithMessage("Please Select an Education Level")
                .NotNull();

            this.RuleFor(_ => _.InstitutionType)
                .GreaterThan(0)
                .WithErrorCode(EmployeeQualificationsErrorCodes.InstitutionTypeLessThanOne)
                .WithMessage("Please Select the Type of Institution")
                .NotNull();

            this.RuleFor(_ => _.DateCompleted)
                .LessThan(DateTime.Now.AddDays(1).Date)
                .WithMessage($"Date Cannot be After {DateTime.Now.AddDays(1).ToShortDateString()}")
                .WithErrorCode(EmployeeQualificationsErrorCodes.InvalidDate);
        }
    }
}
