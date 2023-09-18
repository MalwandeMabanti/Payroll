namespace Payroll.Validators
{
    using FluentValidation;

    using Payroll.Models;

    public class RegisterValidator : AbstractValidator<Register>
    {
        public RegisterValidator()
        {
            this.RuleFor(_ => _.FirstName)
                .NotEmpty()
                .WithMessage("First Name cannot be empty")
                .MinimumLength(2)
                .WithMessage("First Name must be at least 2 characters")
                .MaximumLength(20)
                .WithMessage("First Name cannot be more than 20 characters");

            this.RuleFor(_ => _.LastName)
                .NotEmpty()
                .WithMessage("Last Name cannot be empty")
                .MinimumLength(2)
                .WithMessage("Last Name must be at least 2 characters")
                .MaximumLength(20)
                .WithMessage("Last Name cannot be more than 20 characters");

            this.RuleFor(_ => _.Email)
                .NotEmpty()
                .WithMessage("Email cannot be empty")
                .EmailAddress()
                .WithMessage("Email is not valid");

            this.RuleFor(_ => _.Password)
                .NotEmpty()
                .WithMessage("Password cannot be empty")
                .MinimumLength(6)
                .WithMessage("Password must be at least 6 characters")
                .MaximumLength(20)
                .WithMessage("Password cannot be more than 20 characters");

            this.RuleFor(_ => _.ConfirmPassword)
                .NotEmpty()
                .WithMessage("Confirm Password cannot be empty")
                .Equal(_ => _.Password)
                .WithMessage("Passwords do not match");

            this.RuleFor(_ => _.Role)
                .NotEmpty()
                .WithMessage("Role cannot be empty");
        }
    }
}