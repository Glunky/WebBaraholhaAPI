using FluentValidation;
using WebBaraholkaAPI.Models.Dto.Requests.Auth;

namespace WebBaraholkaAPI.Validation.Auth;

public class SignInValidator : AbstractValidator<SignInRequest>
{
    public SignInValidator()
    {
        RuleFor(r => r.UserInfo)
            .NotEmpty().WithMessage("User Information cannot be empty");

        RuleFor(r => r.UserInfo.Login)
            .NotEmpty().WithMessage("Login is required field")
            .MaximumLength(100).WithMessage("User login is too long");

        RuleFor(r => r.UserInfo.Password)
            .NotEmpty().WithMessage("Password is required field")
            .MinimumLength(8).WithMessage("Password is too short")
            .MaximumLength(30).WithMessage("Password is too long");
    }
}