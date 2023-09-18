namespace Payroll.Tests
{
    using FluentValidation.TestHelper;

    using Payroll.Tests.Helpers;
    using Payroll.Validators;
    using Payroll.Validators.ErrorCodes;

    using Xunit;

    public class EmployeeAddressValidatorTests
    {
        private readonly EmployeeAddressValidator validator;

        public EmployeeAddressValidatorTests()
        {
            this.validator = new EmployeeAddressValidator();
        }

        [Fact]
        public void Create_ShouldValidate_ValidEmployeeAdress()
        {
            var model = EmployeeAddressHelper.EmployeeAddressCreate();

            var result = this.validator.TestValidate(model, _ => _.IncludeRuleSets("default", "Create"));

            Assert.True(result.IsValid, "Expected the EmployeeAddress model to be VALID for 'Create' ruleset");
        }

        [Fact]
        public void Create_ShouldValidate_InValidEmployeeAdress()
        {
            var model = EmployeeAddressHelper.EmployeeAddressCreate();
            model.EmployeeId = 0;

            var result = this.validator.TestValidate(model, _ => _.IncludeRuleSets("default", "Create"));

            Assert.False(result.IsValid, "Expected the EmployeeAddress model to be INVALID for 'Create' ruleset");
        }

        [Fact]
        public void Edit_ShouldValidate_ValidEmployeeAdress()
        {
            var model = EmployeeAddressHelper.EmployeeAddressCreate();

            var result = this.validator.TestValidate(model, _ => _.IncludeRuleSets("default", "Edit"));

            Assert.True(result.IsValid, "Expected the EmployeeAddress model to be INVALID for 'Edit' ruleset");
        }

        [Fact]
        public void Edit_ShouldValidate_InValidEmployeeAdress()
        {
            var model = EmployeeAddressHelper.EmployeeAddressCreate();
            model.AddressId = 0;

            var result = this.validator.TestValidate(model, _ => _.IncludeRuleSets("default", "Edit"));

            Assert.False(result.IsValid, "Expected the EmployeeAddress model to be INVALID for 'Edit' ruleset");
        }

        [Fact]
        public void Delete_ShouldValidate_ValidEmployeeAdress()
        {
            var model = EmployeeAddressHelper.EmployeeAddressCreate();

            var result = this.validator.TestValidate(model, _ => _.IncludeRuleSets("Delete"));

            Assert.True(result.IsValid, "Expected the EmployeeAddress model to be VALID for 'Delete' ruleset");
        }

        [Fact]
        public void Delete_ShouldValidate_InValidEmployeeAdress()
        {
            var model = EmployeeAddressHelper.EmployeeAddressCreate();
            model.AddressId = 0;

            var result = this.validator.TestValidate(model, _ => _.IncludeRuleSets("Delete"));

            Assert.False(result.IsValid, "Expected the EmployeeAddress model to be INVALID for 'Delete' ruleset");
        }

        [Fact]
        public void Create_Valid_EmployeeId()
        {
            var model = EmployeeAddressHelper.EmployeeAddressCreate();
            model.EmployeeId = 1;

            var result = this.validator.TestValidate(model, _ => _.IncludeRuleSets("default", "Create"));

            result.ShouldNotHaveValidationErrorFor(_ => _.EmployeeId);
        }

        [Fact]
        public void Create_Invalid_EmployeeId()
        {
            var model = EmployeeAddressHelper.EmployeeAddressCreate();
            model.EmployeeId = 0;

            var result = this.validator.TestValidate(model, _ => _.IncludeRuleSets("default", "Create"));

            result.ShouldHaveValidationErrorFor(_ => _.EmployeeId)
                .WithErrorCode(EmployeeAddressErrorCodes.EmployeeId);
        }

        [Fact]
        public void Create_Valid_UnitNumber()
        {
            var model = EmployeeAddressHelper.EmployeeAddressCreate();
            model.UnitNumber = "25";

            var result = this.validator.TestValidate(model, _ => _.IncludeRuleSets("default", "Create"));

            result.ShouldNotHaveValidationErrorFor(_ => _.UnitNumber);
        }

        [Fact]
        public void Create_Invalid_UnitNumber()
        {
            var model = EmployeeAddressHelper.EmployeeAddressCreate();
            model.UnitNumber = "012345678901234567890";

            var result = this.validator.TestValidate(model, _ => _.IncludeRuleSets("default", "Create"));

            result.ShouldHaveValidationErrorFor(_ => _.UnitNumber)
                .WithErrorCode(EmployeeAddressErrorCodes.UnitNumber);
        }

        [Fact]
        public void Create_Valid_ComplexName()
        {
            var model = EmployeeAddressHelper.EmployeeAddressCreate();
            model.ComplexName = "Complicated";

            var result = this.validator.TestValidate(model, _ => _.IncludeRuleSets("default", "Create"));

            result.ShouldNotHaveValidationErrorFor(_ => _.ComplexName);
        }

        [Fact]
        public void Create_Invalid_ComplexName()
        {
            var model = EmployeeAddressHelper.EmployeeAddressCreate();
            model.ComplexName = "complexnumcomplexnumcomplexnumcomplexnumcomplexnumc";

            var result = this.validator.TestValidate(model, _ => _.IncludeRuleSets("default", "Create"));

            result.ShouldHaveValidationErrorFor(_ => _.ComplexName)
                .WithErrorCode(EmployeeAddressErrorCodes.ComplexName);
        }

        [Fact]
        public void Create_Valid_StreetNumber()
        {
            var model = EmployeeAddressHelper.EmployeeAddressCreate();
            model.StreetNumber = "666";

            var result = this.validator.TestValidate(model, _ => _.IncludeRuleSets("default", "Create"));

            result.ShouldNotHaveValidationErrorFor(_ => _.StreetNumber);
        }

        [Fact]
        public void Create_Invalid_StreetNumber()
        {
            var model = EmployeeAddressHelper.EmployeeAddressCreate();
            model.StreetNumber = "66688899901";

            var result = this.validator.TestValidate(model, _ => _.IncludeRuleSets("default", "Create"));

            result.ShouldHaveValidationErrorFor(_ => _.StreetNumber)
                .WithErrorCode(EmployeeAddressErrorCodes.StreetNumber);
        }

        [Fact]
        public void Create_Valid_StreetName()
        {
            var model = EmployeeAddressHelper.EmployeeAddressCreate();
            model.StreetName = "Masingafi";

            var result = this.validator.TestValidate(model, _ => _.IncludeRuleSets("default", "Create"));

            result.ShouldNotHaveValidationErrorFor(_ => _.StreetName);
        }

        [Fact]
        public void Create_Invalid_StreetName()
        {
            var model = EmployeeAddressHelper.EmployeeAddressCreate();
            model.StreetName = "Masingafifi";

            var result = this.validator.TestValidate(model, _ => _.IncludeRuleSets("default", "Create"));

            result.ShouldHaveValidationErrorFor(_ => _.StreetName)
                .WithErrorCode(EmployeeAddressErrorCodes.StreetName);
        }

        [Fact]
        public void Create_Valid_Suburb()
        {
            var model = EmployeeAddressHelper.EmployeeAddressCreate();
            model.Suburb = "Mlamlankunzi";

            var result = this.validator.TestValidate(model, _ => _.IncludeRuleSets("default", "Create"));

            result.ShouldNotHaveValidationErrorFor(_ => _.Suburb);
        }

        [Fact]
        public void Create_Invalid_Suburb()
        {
            var model = EmployeeAddressHelper.EmployeeAddressCreate();
            model.Suburb = "lamlankunzlamlankunzlamlankunzlamlankunzlamlankunzi";

            var result = this.validator.TestValidate(model, _ => _.IncludeRuleSets("default", "Create"));

            result.ShouldHaveValidationErrorFor(_ => _.Suburb)
                .WithErrorCode(EmployeeAddressErrorCodes.Suburb);
        }

        [Fact]
        public void Create_Valid_City()
        {
            var model = EmployeeAddressHelper.EmployeeAddressCreate();
            model.City = "Soweto";

            var result = this.validator.TestValidate(model, _ => _.IncludeRuleSets("default", "Create"));

            result.ShouldNotHaveValidationErrorFor(_ => _.City);
        }

        [Fact]
        public void Create_Invalid_City()
        {
            var model = EmployeeAddressHelper.EmployeeAddressCreate();
            model.City = "SowetokasiSowetokasiSowetokasiSowetokasiSowetokasii";

            var result = this.validator.TestValidate(model, _ => _.IncludeRuleSets("default", "Create"));

            result.ShouldHaveValidationErrorFor(_ => _.City)
                .WithErrorCode(EmployeeAddressErrorCodes.City);
        }

        [Fact]
        public void Create_Valid_Code()
        {
            var model = EmployeeAddressHelper.EmployeeAddressCreate();
            model.Code = "1804";

            var result = this.validator.TestValidate(model, _ => _.IncludeRuleSets("default", "Create"));

            result.ShouldNotHaveValidationErrorFor(_ => _.Code);
        }

        [Fact]
        public void Create_Invalid_Code()
        {
            var model = EmployeeAddressHelper.EmployeeAddressCreate();
            model.Code = "18041804991";

            var result = this.validator.TestValidate(model, _ => _.IncludeRuleSets("default", "Create"));

            result.ShouldHaveValidationErrorFor(_ => _.Code)
                .WithErrorCode(EmployeeAddressErrorCodes.Code);
        }

        [Fact]
        public void Create_Valid_CountryId()
        {
            var model = EmployeeAddressHelper.EmployeeAddressCreate();
            model.CountryId = 1;

            var result = this.validator.TestValidate(model, _ => _.IncludeRuleSets("default", "Create"));

            result.ShouldNotHaveValidationErrorFor(_ => _.CountryId);
        }

        [Fact]
        public void Create_Invalid_CountryId()
        {
            var model = EmployeeAddressHelper.EmployeeAddressCreate();
            model.CountryId = 0;

            var result = this.validator.TestValidate(model, _ => _.IncludeRuleSets("default", "Create"));

            result.ShouldHaveValidationErrorFor(_ => _.CountryId)
                .WithErrorCode(EmployeeAddressErrorCodes.CountryId);
        }
    }
}
