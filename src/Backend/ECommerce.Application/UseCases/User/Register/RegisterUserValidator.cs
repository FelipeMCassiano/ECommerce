using Communication.Requests;
using FluentValidation;

namespace ECommerce.Application.UseCases.User.Register;

public class RegisterUserValidator : AbstractValidator<UserRequest>
{
    public RegisterUserValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
    
}