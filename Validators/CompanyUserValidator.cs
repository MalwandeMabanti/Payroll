namespace Payroll.Validators
{
    using FluentValidation;

    using Payroll.Models;

    public class CompanyUserValidator : AbstractValidator<CompanyUser>
    {
        public CompanyUserValidator()
        {
            this.RuleSet("Edit", () =>
            {
                this.GeneralRules();

                this.RuleFor(_ => _.CompanyUserId)
                    .GreaterThan(0);
            });

            this.RuleSet("Create", () =>
            {
                this.GeneralRules();
            });

            this.RuleSet("Delete", () =>
            {
                this.RuleFor(_ => _.CompanyUserId)
                    .GreaterThan(0)
                    .WithMessage("Could not delete user.");
            });
        }

        private void GeneralRules()
        {
            this.RuleFor(_ => _.CompanyUserFirstName)
                .NotEmpty()
                .MaximumLength(50);

            this.RuleFor(_ => _.CompanyUserLastName)
                .NotEmpty()
                .MaximumLength(50);

            this.RuleFor(_ => _.Email)
                .EmailAddress()
                .NotEmpty();
        }
    }
}
