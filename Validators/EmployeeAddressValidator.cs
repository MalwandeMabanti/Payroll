namespace Payroll.Validators
{
    using FluentValidation;

    using Payroll.Models;
    using Payroll.Validators.ErrorCodes;

    public class EmployeeAddressValidator : AbstractValidator<EmployeeAddress>
    {
        public EmployeeAddressValidator()
        {
            this.GeneralRules();

            this.RuleSet("Edit", () =>
            {
                this.RuleFor(_ => _.AddressId)
                .GreaterThan(0)
                .WithErrorCode(EmployeeAddressErrorCodes.AddressId);
            });

            this.RuleSet("Create", () =>
            {
            });

            this.RuleSet("Delete", () =>
            {
                this.RuleFor(_ => _.AddressId)
                .GreaterThan(0)
                .WithErrorCode(EmployeeAddressErrorCodes.AddressId);
            });
        }

        private void GeneralRules()
        {
            this.RuleFor(_ => _.EmployeeId)
                .GreaterThan(0)
                .WithErrorCode(EmployeeAddressErrorCodes.EmployeeId);

            this.RuleFor(_ => _.UnitNumber)
                .MaximumLength(20)
                .WithErrorCode(EmployeeAddressErrorCodes.UnitNumber);

            this.RuleFor(_ => _.ComplexName)
                .MaximumLength(50)
                .WithErrorCode(EmployeeAddressErrorCodes.ComplexName);

            this.RuleFor(_ => _.StreetNumber)
                .MaximumLength(10)
                .WithErrorCode(EmployeeAddressErrorCodes.StreetNumber);

            this.RuleFor(_ => _.StreetName)
                .NotEmpty()
                .MaximumLength(10)
                .WithErrorCode(EmployeeAddressErrorCodes.StreetName);

            this.RuleFor(_ => _.Suburb)
                .MaximumLength(50)
                .WithErrorCode(EmployeeAddressErrorCodes.Suburb);

            this.RuleFor(_ => _.City)
                .NotEmpty()
                .MaximumLength(50)
                .WithErrorCode(EmployeeAddressErrorCodes.City);

            this.RuleFor(_ => _.Code)
                .NotEmpty()
                .MaximumLength(10)
                .WithErrorCode(EmployeeAddressErrorCodes.Code);

            this.RuleFor(_ => _.CountryId)
                .GreaterThan(0)
                .WithErrorCode(EmployeeAddressErrorCodes.CountryId);
        }
    }
}
