using FluentValidation;
using WebBaraholkaAPI.Models.Dto.Requests.Auth;

namespace WebBaraholkaAPI.Validation.Auth;

public class SignUpValidator : AbstractValidator<SignUpRequest>
{
    // TODO: add password regex
    public SignUpValidator()
    {
        RuleFor(r => r.ConsumerInformation)
            .NotEmpty().WithMessage("Consumer Information cannot be empty");

        RuleFor(r => r.ConsumerInformation.Login)
            .NotEmpty().WithMessage("Name is required field")
            .MaximumLength(100).WithMessage("Consumer name is too long");

        RuleFor(r => r.ConsumerInformation.Password)
            .NotEmpty().WithMessage("Password is required field")
            .MinimumLength(8).WithMessage("Password is too short")
            .MaximumLength(30).WithMessage("Password is too long");

        RuleFor(r => r.ConsumerInformation.Email)
            .NotEmpty().WithMessage("Email is required field")
            .EmailAddress().WithMessage("Email address is incorrect");
    }
}