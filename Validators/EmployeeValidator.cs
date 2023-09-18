namespace Payroll.Validators
{
    using FluentValidation;

    using Payroll.Models;
    using Payroll.Validators.ErrorCodes;

    public class EmployeeValidator : AbstractValidator<Employee>
    {
        public EmployeeValidator()
        {
            this.RuleSet("Edit", () =>
            {
                this.GeneralRules();

                this.RuleFor(_ => _.EmployeeId)
                    .GreaterThan(0)
                    .WithErrorCode(EmployeeErrorCodes.EmployeeId);
            });

            this.RuleSet("Create", () =>
            {
                this.GeneralRules();
            });

            this.RuleSet("Delete", () =>
            {
                this.RuleFor(_ => _.EmployeeId)
                    .GreaterThan(0)
                    .WithErrorCode(EmployeeErrorCodes.EmployeeId)
                    .WithMessage("Could not delete employee.");
            });

            this.GeneralRules();
        }

        private void GeneralRules()
        {
            this.RuleFor(_ => _.EmployeeNumber)
                .NotEmpty()
                .MaximumLength(20)
                .WithErrorCode(EmployeeErrorCodes.EmployeeNumber);

            this.RuleFor(_ => _.LastName)
                .NotEmpty()
                .MaximumLength(50)
                .WithErrorCode(EmployeeErrorCodes.LastName);

            this.RuleFor(_ => _.FirstName)
                .NotEmpty()
                .MaximumLength(50)
                .WithErrorCode(EmployeeErrorCodes.FirstName);

            this.RuleFor(_ => _.Email)
                .NotEmpty()
                .EmailAddress()
                .WithErrorCode(EmployeeErrorCodes.Email)
                .MaximumLength(50)
                .WithErrorCode(EmployeeErrorCodes.Email);

            this.RuleFor(_ => _.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(50);

            this.RuleFor(_ => _.MiddleName)
                .MaximumLength(50)
                .WithErrorCode(EmployeeErrorCodes.MiddleName);

            this.RuleFor(_ => _.Initials)
                .NotEmpty()
                .Length(1, 3)
                .WithErrorCode(EmployeeErrorCodes.Initials);

            this.RuleFor(_ => _.PreferredName)
                .NotEmpty()
                .MaximumLength(50)
                .WithErrorCode(EmployeeErrorCodes.PreferredName);

            this.RuleFor(_ => _.MaidenName)
                .MaximumLength(50)
                .WithErrorCode(EmployeeErrorCodes.MaidenName);

            this.RuleFor(_ => _.TitleId)
                .GreaterThan(0)
                .WithMessage("Please pick a valid title")
                .WithErrorCode(EmployeeErrorCodes.TitleId);

            this.RuleFor(_ => _.EmployeeRetired)
                .NotNull()
                .WithErrorCode(EmployeeErrorCodes.EmployeeRetired);
        }
    }
}
