namespace Payroll.Validators
{
    using FluentValidation;

    using Payroll.Models;
    using Payroll.Validators.ErrorCodes;

    public class EmployeeDependantValidator : AbstractValidator<EmployeeDependant>
    {
        public EmployeeDependantValidator()
        {
            this.RuleSet("Create", () =>
            {
                this.GeneralRules();
            });

            this.RuleSet("Edit", () =>
            {
                this.GeneralRules();

                this.RuleFor(_ => _.EmployeeDependantID)
                    .GreaterThan(0)
                    .WithErrorCode(EmployeeDependantErrorCodes.EmployeeDependantID);
            });

            this.RuleSet("Delete", () =>
            {
                this.RuleFor(_ => _.EmployeeDependantID)
                    .GreaterThan(0)
                    .WithErrorCode(EmployeeDependantErrorCodes.EmployeeDependantID);
            });
        }

        private void GeneralRules()
        {
            this.RuleFor(_ => _.EmployeeId)
                .GreaterThan(0)
                .WithErrorCode(EmployeeDependantErrorCodes.EmployeeID);

            this.RuleFor(_ => _.EffectiveDate.Year)
                .GreaterThan(1900)
                .WithErrorCode(EmployeeDependantErrorCodes.EffectiveDate)
                .WithMessage("Effective date year cannot be less than 1900.");

            this.RuleFor(_ => _.FirstName)
                .NotEmpty()
                .WithErrorCode(EmployeeDependantErrorCodes.NameNotEmpty)
                .MaximumLength(50)
                .WithMessage("First Name cannot be greater than 50 characters.")
                .WithErrorCode(EmployeeDependantErrorCodes.NameMax);

            this.RuleFor(_ => _.LastName)
                .NotEmpty()
                .WithErrorCode(EmployeeDependantErrorCodes.NameNotEmpty)
                .MaximumLength(50)
                .WithMessage("Last Name cannot be greater than 50 characters.")
                .WithErrorCode(EmployeeDependantErrorCodes.NameMax);

            this.RuleFor(_ => _.ContactNumber)
                .GreaterThan(0)
                .WithMessage("Contact Number must be greater than 0")
                .WithErrorCode(EmployeeDependantErrorCodes.ContactNumber);

            this.RuleFor(_ => _.DependantTypeID)
                .GreaterThan(0)
                .WithMessage("Dependant Type ID must be 1,2 or 3")
                .WithErrorCode(EmployeeDependantErrorCodes.DependantTypeID);

            this.RuleFor(_ => _.BirthDate)
                .LessThan(DateTime.Now.AddYears(-18).AddDays(-1))
                .WithErrorCode(EmployeeDependantErrorCodes.BirthDateUnder18)
                .GreaterThan(DateTime.Now.AddYears(-120).AddDays(1))
                .WithErrorCode(EmployeeDependantErrorCodes.BirthDateOver120)
                .When(_ => _.DependantTypeID.Equals((int)DependantType.Adult) || _.DependantTypeID.Equals((int)DependantType.Spouse))
                .WithMessage("Adults and Spouses must be older than 18 years old");

            this.RuleFor(_ => _.BirthDate)
                .GreaterThan(DateTime.Now.AddYears(-21).AddDays(1))
                .WithErrorCode(EmployeeDependantErrorCodes.BirthDateOver21)
                .When(_ => _.DependantTypeID.Equals((int)DependantType.Child))
                .WithMessage("Children must be younger than 21 years old");
        }
    }
}