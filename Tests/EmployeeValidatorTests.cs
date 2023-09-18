namespace Payroll.Tests
{
    using FluentValidation.TestHelper;

    using Payroll.Tests.Helpers;
    using Payroll.Validators;
    using Payroll.Validators.ErrorCodes;

    using Xunit;

    public class EmployeeValidatorTests
    {
        private readonly EmployeeValidator validator;

        public EmployeeValidatorTests()
        {
            this.validator = new EmployeeValidator();
        }

        [Fact]
        public void Edit_ShouldValidate_ValidEmployee()
        {
            var model = EmployeeHelper.CreateEmployee();

            var result = this.validator.TestValidate(model, _ => _.IncludeRuleSets("Edit"));

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Edit_ShouldValidate_InvalidEmployee()
        {
            var model = EmployeeHelper.CreateEmployee();
            model.EmployeeId = 0;

            var result = this.validator.TestValidate(model, _ => _.IncludeRuleSets("Edit"));

            result.ShouldHaveValidationErrorFor(_ => _.EmployeeId)
                .WithErrorCode(EmployeeErrorCodes.EmployeeId);
        }

        [Fact]
        public void Create_ShouldValidate_ValidEmployee()
        {
            var model = EmployeeHelper.CreateEmployee();

            var result = this.validator.TestValidate(model, _ => _.IncludeRuleSets("Create"));

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Create_ShouldValidate_InvalidEmployee()
        {
            var model = EmployeeHelper.CreateEmployee();
            model.EmployeeNumber = "EMP0000011111222223333344444";

            var result = this.validator.TestValidate(model, _ => _.IncludeRuleSets("Create"));

            result.ShouldHaveValidationErrorFor(x => x.EmployeeNumber)
                .WithErrorCode(EmployeeErrorCodes.EmployeeNumber);
        }

        [Fact]
        public void Delete_ShouldValidate_ValidEmployeeId()
        {
            var model = EmployeeHelper.CreateEmployee();

            var result = this.validator.TestValidate(model, _ => _.IncludeRuleSets("Delete"));

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Delete_ShouldValidate_InvalidEmployeeId()
        {
            var model = EmployeeHelper.CreateEmployee();
            model.EmployeeId = 0;

            var result = this.validator.TestValidate(model, _ => _.IncludeRuleSets("Delete"));

            result.ShouldHaveValidationErrorFor(_ => _.EmployeeId)
                .WithErrorCode(EmployeeErrorCodes.EmployeeId);
        }

        [Fact]
        public void GeneralRules_EmployeeNumber_ShouldValidate_ValidEmployeeNumber()
        {
            var model = EmployeeHelper.CreateEmployee();

            var result = this.validator.TestValidate(model, _ => _.IncludeRuleSets("Create"));

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void GeneralRules_EmployeeNumber_ShouldValidate_InvalidEmployeeNumber()
        {
            var model = EmployeeHelper.CreateEmployee();
            model.EmployeeNumber = "EMP0000011111222223333344444";

            var result = this.validator.TestValidate(model, _ => _.IncludeRuleSets("Create"));

            result.ShouldHaveValidationErrorFor(_ => _.EmployeeNumber)
                .WithErrorCode(EmployeeErrorCodes.EmployeeNumber);
        }

        [Fact]
        public void GeneralRules_LastName_ShouldValidate_ValidLastName()
        {
            var model = EmployeeHelper.CreateEmployee();

            var result = this.validator.TestValidate(model, _ => _.IncludeRuleSets("Create"));

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void GeneralRules_LastName_ShouldValidate_InvalidLastName()
        {
            var model = EmployeeHelper.CreateEmployee();
            model.LastName = "Tttttttttthhhhhhhhhheeeeeeeee"
                + "Nnnnnnnnnnndddddddddd"
                + "duuuuuuuuuuuuuuuuuuuusssssssssss";

            var result = this.validator.TestValidate(model, _ => _.IncludeRuleSets("Create"));

            result.ShouldHaveValidationErrorFor(_ => _.LastName)
                .WithErrorCode(EmployeeErrorCodes.LastName);
        }

        [Fact]
        public void GeneralRules_FirstName_ShouldValidate_ValidFirstName()
        {
            var model = EmployeeHelper.CreateEmployee();

            var result = this.validator.TestValidate(model, _ => _.IncludeRuleSets("Create"));

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void GeneralRules_FirstName_ShouldValidate_InvalidFirstName()
        {
            var model = EmployeeHelper.CreateEmployee();
            model.FirstName = "Tttttttttthhhhhhhhhheeeeeeeee"
                + "Nnnnnnnnnnndddddddddd"
                + "duuuuuuuuuuuuuuuuuuuusssssssssss";

            var result = this.validator.TestValidate(model, _ => _.IncludeRuleSets("Create"));

            result.ShouldHaveValidationErrorFor(_ => _.FirstName)
                .WithErrorCode(EmployeeErrorCodes.FirstName);
        }

        [Fact]
        public void GeneralRules_Email_ShouldValidate_ValidEmail()
        {
            var model = EmployeeHelper.CreateEmployee();

            var result = this.validator.TestValidate(model, _ => _.IncludeRuleSets("Create"));

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void GeneralRules_Email_ShouldValidate_InvalidEmail()
        {
            var model = EmployeeHelper.CreateEmployee();
            model.Email = "test";

            var result = this.validator.TestValidate(model, _ => _.IncludeRuleSets("Create"));

            result.ShouldHaveValidationErrorFor(_ => _.Email)
                .WithErrorCode(EmployeeErrorCodes.Email);
        }

        [Fact]
        public void GeneralRules_MiddleName_ShouldValidate_ValidMiddleName()
        {
            var model = EmployeeHelper.CreateEmployee();

            var result = this.validator.TestValidate(model, _ => _.IncludeRuleSets("Create"));

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void GeneralRules_MiddleName_ShouldValidate_InvalidMiddleName()
        {
            var model = EmployeeHelper.CreateEmployee();
            model.MiddleName = "Tttttttttthhhhhhhhhheeeeeeeee"
                + "Nnnnnnnnnnndddddddddd"
                + "duuuuuuuuuuuuuuuuuuuusssssssssss";

            var result = this.validator.TestValidate(model, _ => _.IncludeRuleSets("Create"));

            result.ShouldHaveValidationErrorFor(_ => _.MiddleName)
                .WithErrorCode(EmployeeErrorCodes.MiddleName);
        }

        [Fact]
        public void GeneralRules_Initials_ShouldValidate_ValidInitials()
        {
            var model = EmployeeHelper.CreateEmployee();

            var result = this.validator.TestValidate(model, _ => _.IncludeRuleSets("Create"));

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void GeneralRules_Initials_ShouldValidate_InvalidInitials()
        {
            var model = EmployeeHelper.CreateEmployee();
            model.Initials = "THENDUUS";

            var result = this.validator.TestValidate(model, _ => _.IncludeRuleSets("Create"));

            result.ShouldHaveValidationErrorFor(_ => _.Initials)
                .WithErrorCode(EmployeeErrorCodes.Initials);
        }

        [Fact]
        public void GeneralRules_PreferredName_ShouldValidate_ValidPreferredName()
        {
            var model = EmployeeHelper.CreateEmployee();

            var result = this.validator.TestValidate(model, _ => _.IncludeRuleSets("Create"));

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void GeneralRules_PreferredName_ShouldValidate_InvalidPreferredName()
        {
            var model = EmployeeHelper.CreateEmployee();
            model.PreferredName = "Tttttttttthhhhhhhhhheeeeeeeee"
                + "Nnnnnnnnnnndddddddddd"
                + "duuuuuuuuuuuuuuuuuuuusssssssssss";
            var result = this.validator.TestValidate(model, _ => _.IncludeRuleSets("Create"));

            result.ShouldHaveValidationErrorFor(_ => _.PreferredName)
                .WithErrorCode(EmployeeErrorCodes.PreferredName);
        }

        [Fact]
        public void GeneralRules_MaidenName_ShouldValidate_ValidMaidenName()
        {
            var model = EmployeeHelper.CreateEmployee();

            var result = this.validator.TestValidate(model, _ => _.IncludeRuleSets("Create"));

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void GeneralRules_MaidenName_ShouldValidate_InvalidMaidenName()
        {
            var model = EmployeeHelper.CreateEmployee();
            model.MaidenName = "Tttttttttthhhhhhhhhheeeeeeeee"
                + "Nnnnnnnnnnndddddddddd"
                + "duuuuuuuuuuuuuuuuuuuusssssssssss";

            var result = this.validator.TestValidate(model, _ => _.IncludeRuleSets("Create"));

            result.ShouldHaveValidationErrorFor(_ => _.MaidenName)
                .WithErrorCode(EmployeeErrorCodes.MaidenName);
        }

        [Fact]
        public void GeneralRules_TitleId_ShouldValidate_ValidTitleId()
        {
            var model = EmployeeHelper.CreateEmployee();

            var result = this.validator.TestValidate(model, _ => _.IncludeRuleSets("Create"));

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void GeneralRules_TitleId_ShouldValidate_InvalidTitleId()
        {
            var model = EmployeeHelper.CreateEmployee();
            model.TitleId = 0;

            var result = this.validator.TestValidate(model, _ => _.IncludeRuleSets("Create"));

            result.ShouldHaveValidationErrorFor(_ => _.TitleId)
                .WithErrorCode(EmployeeErrorCodes.TitleId);
        }

        [Fact]
        public void GeneralRules_EmployeeRetired_ShouldValidate_ValidEmployeeRetired()
        {
            var model = EmployeeHelper.CreateEmployee();

            var result = this.validator.TestValidate(model, _ => _.IncludeRuleSets("Create"));

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void GeneralRules_EmployeeRetired_ShouldValidate_InvalidEmployeeRetired()
        {
            var model = EmployeeHelper.CreateEmployee();
            model.EmployeeRetired = null;

            var result = this.validator.TestValidate(model, _ => _.IncludeRuleSets("Create"));

            result.ShouldHaveValidationErrorFor(_ => _.EmployeeRetired)
                .WithErrorCode(EmployeeErrorCodes.EmployeeRetired);
        }
    }
}
