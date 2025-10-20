using ecommerce.Models;
using FluentValidation;

namespace ecommerce.Validators
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Customer name is required.")
                .MinimumLength(3).WithMessage("Customer name must be at least 3 characters.");

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(c => c.Phone)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\d{10,15}$").WithMessage("Phone number must be between 10–15 digits.");
        }
    }
}
