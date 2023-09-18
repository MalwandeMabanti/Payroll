namespace Payroll.Tests
{
    using FluentValidation.TestHelper;

    using Payroll.Tests.Helper;
    using Payroll.Validators;
    using Payroll.Validators.ErrorCodes;

    using Xunit;

    public class EmployeeDependantValidatorTest
    {
        private readonly EmployeeDependantValidator validator;

        public EmployeeDependantValidatorTest()
        {
            this.validator = new EmployeeDependantValidator();
        }

        [Fact]
        public void Create_ShouldValidate_ValidEmployeeDependant()
        {
            var employeeDependant = EmployeeDependantHelper.CreateEmployeeDependant();

            var result = this.validator.TestValidate(employeeDependant, _ => _.IncludeRuleSets("Create"));

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Create_ShouldNotValidate_InvalidEmployeeDependant()
        {
            var employeeDependant = EmployeeDependantHelper.CreateEmployeeDependant();
            var dependantTypeIDNotExist = 4;

            employeeDependant.DependantTypeID = dependantTypeIDNotExist;

            var result = this.validator.TestValidate(employeeDependant, _ => _.IncludeRuleSets("Create"));

            result.ShouldHaveValidationErrorFor(_ => _.DependantTypeID)
                .WithErrorCode(EmployeeDependantErrorCodes.DependantTypeID);
        }

        [Fact]
        public void Edit_ShouldValidate_ValidEmployeeDependant()
        {
            var employeeDependant = EmployeeDependantHelper.CreateEmployeeDependant();

            var result = this.validator.TestValidate(employeeDependant, _ => _.IncludeRuleSets("Edit"));

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Edit_ShouldNotValidate_InValidEmployeeDependant()
        {
            var employeeDependant = EmployeeDependantHelper.CreateEmployeeDependant();

            employeeDependant.EmployeeDependantID = 0;

            var result = this.validator.TestValidate(employeeDependant, _ => _.IncludeRuleSets("Edit"));

            result.ShouldHaveValidationErrorFor(_ => _.EmployeeDependantID)
                .WithErrorCode(EmployeeDependantErrorCodes.EmployeeDependantID);
        }

        [Fact]
        public void Delete_ShouldValidate_ValidEmployeeDependant()
        {
            var employeeDependant = EmployeeDependantHelper.CreateEmployeeDependant();

            var result = this.validator.TestValidate(employeeDependant, _ => _.IncludeRuleSets("Delete"));

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Delete_ShouldNotValidate_InValidEmployeeDependant()
        {
            var employeeDependant = EmployeeDependantHelper.CreateEmployeeDependant();

            employeeDependant.EmployeeDependantID = 0;

            var result = this.validator.TestValidate(employeeDependant, _ => _.IncludeRuleSets("Delete"));

            result.ShouldHaveValidationErrorFor(_ => _.EmployeeDependantID)
                .WithErrorCode(EmployeeDependantErrorCodes.EmployeeDependantID);
        }

        [Fact]
        public void GeneralRules_ShouldValidate_ValidEmployeeID()
        {
            var employeeDependant = EmployeeDependantHelper.CreateEmployeeDependant();

            employeeDependant.EmployeeId = 2;

            var result = this.validator.TestValidate(employeeDependant, _ => _.IncludeRuleSets("Create"));

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void GeneralRules_ShouldNotValidate_InValidEmployeeID()
        {
            var employeeDependant = EmployeeDependantHelper.CreateEmployeeDependant();

            employeeDependant.EmployeeId = 0;

            var result = this.validator.TestValidate(employeeDependant, _ => _.IncludeRuleSets("Create"));

            result.ShouldHaveValidationErrorFor(_ => _.EmployeeId)
                .WithErrorCode(EmployeeDependantErrorCodes.EmployeeID);
        }

        [Fact]
        public void GeneralRules_ShouldValidate_ValidEffectiveDate()
        {
            var employeeDependant = EmployeeDependantHelper.CreateEmployeeDependant();

            employeeDependant.EffectiveDate = DateTime.Parse("01-04-2023");

            var result = this.validator.TestValidate(employeeDependant, _ => _.IncludeRuleSets("Create"));

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void GeneralRules_ShouldNotValidate_InValidEffectiveDate()
        {
            var employeeDependant = EmployeeDependantHelper.CreateEmployeeDependant();

            employeeDependant.EffectiveDate = DateTime.Parse("01-04-1800");

            var result = this.validator.TestValidate(employeeDependant, _ => _.IncludeRuleSets("Create"));

            result.ShouldHaveValidationErrorFor(_ => _.EffectiveDate.Year)
                .WithErrorCode(EmployeeDependantErrorCodes.EffectiveDate);
        }

        [Fact]
        public void GeneralRules_ShouldValidate_ValidFirstName()
        {
            var employeeDependant = EmployeeDependantHelper.CreateEmployeeDependant();

            employeeDependant.FirstName = "FirstName";

            var result = this.validator.TestValidate(employeeDependant, _ => _.IncludeRuleSets("Create"));

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void GeneralRules_ShouldNotValidate_InValidFirstName()
        {
            var employeeDependant = EmployeeDependantHelper.CreateEmployeeDependant();

            employeeDependant.FirstName = "FirstNameeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee";

            var result = this.validator.TestValidate(employeeDependant, options => options.IncludeRuleSets("Create"));

            result.ShouldHaveValidationErrorFor(_ => _.FirstName)
                .WithErrorCode(EmployeeDependantErrorCodes.NameMax);
        }

        [Fact]
        public void GeneralRules_ShouldValidate_ValidLastName()
        {
            var employeeDependant = EmployeeDependantHelper.CreateEmployeeDependant();

            employeeDependant.LastName = "LastName";

            var result = this.validator.TestValidate(employeeDependant, _ => _.IncludeRuleSets("Create"));

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void GeneralRules_ShouldNotValidate_InValidLastName()
        {
            var employeeDependant = EmployeeDependantHelper.CreateEmployeeDependant();

            employeeDependant.LastName = string.Empty;

            var result = this.validator.TestValidate(employeeDependant, _ => _.IncludeRuleSets("Create"));

            result.ShouldHaveValidationErrorFor(_ => _.LastName)
                .WithErrorCode(EmployeeDependantErrorCodes.NameNotEmpty);
        }

        [Fact]
        public void GeneralRules_ShouldValidate_ValidContactNumber()
        {
            var employeeDependant = EmployeeDependantHelper.CreateEmployeeDependant();

            employeeDependant.ContactNumber = 1234567890;

            var result = this.validator.TestValidate(employeeDependant, _ => _.IncludeRuleSets("Edit"));

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void GeneralRules_ShouldNotValidate_InValidContactNumber()
        {
            var employeeDependant = EmployeeDependantHelper.CreateEmployeeDependant();

            employeeDependant.ContactNumber = 0;

            var result = this.validator.TestValidate(employeeDependant, _ => _.IncludeRuleSets("Edit"));

            result.ShouldHaveValidationErrorFor(_ => _.ContactNumber)
                .WithErrorCode(EmployeeDependantErrorCodes.ContactNumber);
        }

        [Fact]
        public void GeneralRules_ShouldValidate_ValidChildEmployeeDependant()
        {
            var employeeDependant = EmployeeDependantHelper.CreateEmployeeDependant();

            var result = this.validator.TestValidate(employeeDependant, _ => _.IncludeRuleSets("Create"));

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void GeneralRules_ShouldNotValidate_InvalidChildEmployeeDependant()
        {
            var employeeDependant = EmployeeDependantHelper.CreateEmployeeDependant();

            employeeDependant.BirthDate = DateTime.Parse("12-04-1990");

            var result = this.validator.TestValidate(employeeDependant, _ => _.IncludeRuleSets("Create"));

            result.ShouldHaveValidationErrorFor(_ => _.BirthDate)
                .WithErrorCode(EmployeeDependantErrorCodes.BirthDateOver21);
        }

        [Fact]
        public void GeneralRules_ShouldValidate_ValidAdultEmployeeDependant()
        {
            var employeeDependant = EmployeeDependantHelper.CreateEmployeeDependant();

            employeeDependant.BirthDate = DateTime.Parse("12-04-1997");
            employeeDependant.DependantTypeID = 2;

            var result = this.validator.TestValidate(employeeDependant, _ => _.IncludeRuleSets("Create"));

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void GeneralRules_ShouldNotValidate_InvalidAdultEmployeeDependant_TooYoung()
        {
            var employeeDependant = EmployeeDependantHelper.CreateEmployeeDependant();

            employeeDependant.DependantTypeID = 2;
            employeeDependant.BirthDate = DateTime.Parse("12-04-2022");

            var result = this.validator.TestValidate(employeeDependant, _ => _.IncludeRuleSets("Create"));

            result.ShouldHaveValidationErrorFor(_ => _.BirthDate)
                .WithErrorCode(EmployeeDependantErrorCodes.BirthDateUnder18);
        }

        [Fact]
        public void GeneralRules_ShouldNotValidate_InvalidAdultEmployeeDependant_TooOld()
        {
            var employeeDependant = EmployeeDependantHelper.CreateEmployeeDependant();

            employeeDependant.DependantTypeID = 1;
            employeeDependant.BirthDate = DateTime.Parse("01-01-1900");

            var result = this.validator.TestValidate(employeeDependant, _ => _.IncludeRuleSets("Create"));

            result.ShouldHaveValidationErrorFor(_ => _.BirthDate)
                .WithErrorCode(EmployeeDependantErrorCodes.BirthDateOver120);
        }
    }
}
