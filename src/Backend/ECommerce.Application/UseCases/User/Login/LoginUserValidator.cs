using Communication.Requests;
using FluentValidation;

namespace ECommerce.Application.UseCases.User.Login;

public class LoginUserValidator : AbstractValidator<LoginRequest>
{
    public LoginUserValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty();
    }
    
}