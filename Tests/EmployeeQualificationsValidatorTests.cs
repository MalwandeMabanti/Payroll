namespace Payroll.Tests
{
    using FluentValidation.TestHelper;

    using Payroll.Tests.Helpers;
    using Payroll.Validators;
    using Payroll.Validators.ErrorCodes;

    using Xunit;

    public class EmployeeQualificationsValidatorTests
    {
        private readonly EmployeeQualificationValidator validator;

        public EmployeeQualificationsValidatorTests()
        {
            this.validator = new EmployeeQualificationValidator();
        }

        [Fact]
        public void Create_ShouldValidate_Valid_EmployeeQualification()
        {
            var model = EmployeeQualificationsHelper.GetValidModel();

            var result = this.validator.TestValidate(model, _ => _.IncludeRuleSets("Create"));

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Create_ShouldValidate_Invalid_EmployeeQualification_DateCompleted()
        {
            var model = EmployeeQualificationsHelper.GetValidModel();
            model.DateCompleted = DateTime.Now.AddDays(3);

            var result = this.validator.TestValidate(model, _ => _.IncludeRuleSets("Create"));

            result.ShouldHaveValidationErrorFor(_ => _.DateCompleted)
                  .WithErrorCode(EmployeeQualificationsErrorCodes.InvalidDate);
        }

        [Fact]
        public void Create_ShouldValidate_Invalid_EmployeeQualification_EmpId()
        {
            var model = EmployeeQualificationsHelper.GetValidModel();
            model.EmpId = -1;

            var result = this.validator.TestValidate(model, _ => _.IncludeRuleSets("Create"));

            result.ShouldHaveValidationErrorFor(_ => _.EmpId)
                  .WithErrorCode(EmployeeQualificationsErrorCodes.EmpIdLessThanOne);
        }

        [Fact]
        public void Create_ShouldValidate_Invalid_EmployeeQualification_Institution()
        {
            var model = EmployeeQualificationsHelper.GetValidModel();
            model.Institution = "Mi, id sollicitudin urna fermentum ut fusce varius nisl ac ipsum gravida vel pretium tellus.";

            var result = this.validator.TestValidate(model, _ => _.IncludeRuleSets("Create"));

            result.ShouldHaveValidationErrorFor(_ => _.Institution)
                  .WithErrorCode(EmployeeQualificationsErrorCodes.InstitutionLengthOverLimit);
        }

        [Fact]
        public void Create_ShouldValidate_Invalid_EmployeeQualification_QaulificationID()
        {
            var model = EmployeeQualificationsHelper.GetValidModel();
            model.QualificationID = 0;

            var result = this.validator.TestValidate(model, _ => _.IncludeRuleSets("Create"));

            result.ShouldHaveValidationErrorFor(_ => _.QualificationID)
                  .WithErrorCode(EmployeeQualificationsErrorCodes.QualificationIdLessThanOne);
        }

        [Fact]
        public void Create_ShouldValidate_Invalid_EmployeeQualification_EducationLevelID()
        {
            var model = EmployeeQualificationsHelper.GetValidModel();
            model.EducationLevelID = -1;

            var result = this.validator.TestValidate(model, _ => _.IncludeRuleSets("Create"));

            result.ShouldHaveValidationErrorFor(_ => _.EducationLevelID)
                  .WithErrorCode(EmployeeQualificationsErrorCodes.EducationLevelIdLessThanOne);
        }

        [Fact]
        public void Create_ShouldValidate_Invalid_EmployeeQualification_InstitutionType()
        {
            var model = EmployeeQualificationsHelper.GetValidModel();
            model.InstitutionType = -1;

            var result = this.validator.TestValidate(model, _ => _.IncludeRuleSets("Create"));

            result.ShouldHaveValidationErrorFor(_ => _.InstitutionType)
                  .WithErrorCode(EmployeeQualificationsErrorCodes.InstitutionTypeLessThanOne);
        }

        [Fact]
        public void Edit_ShouldValidate_Valid_EmployeeQualification()
        {
            var model = EmployeeQualificationsHelper.GetValidModel();

            var result = this.validator.TestValidate(model, _ => _.IncludeRuleSets("Edit"));

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Edit_ShouldValidate_Invalid_EmployeeQualification_EmployeeQualificationID()
        {
            var model = EmployeeQualificationsHelper.GetValidModel();
            model.EmployeeQualificationID = 0;

            var result = this.validator.TestValidate(model, _ => _.IncludeRuleSets("Edit"));

            result.ShouldHaveValidationErrorFor(_ => _.EmployeeQualificationID)
                  .WithErrorCode(EmployeeQualificationsErrorCodes.EmployeeQualificationIdLessThanOne);
        }

        [Fact]
        public void Delete_ShouldValidate_Valid_EmployeeQualification()
        {
            var model = EmployeeQualificationsHelper.GetValidModel();

            var result = this.validator.TestValidate(model, _ => _.IncludeRuleSets("Delete"));

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Delete_ShouldValidate_Invalid_EmployeeQualification()
        {
            var model = EmployeeQualificationsHelper.GetValidModel();
            model.EmployeeQualificationID = -1;

            var result = this.validator.TestValidate(model, _ => _.IncludeRuleSets("Delete"));

            result.ShouldHaveValidationErrorFor(_ => _.EmployeeQualificationID)
                  .WithErrorCode(EmployeeQualificationsErrorCodes.EmployeeQualificationIdLessThanOne);
        }
    }
}
