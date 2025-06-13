using System.Security.Authentication;
using Communication.Requests;
using Communication.Responses;
using ECommerce.Domain.Repositories;
using ECommerce.Domain.Security.Cryptography;
using ECommerce.Domain.Security.Tokens;
using Exceptions.BaseExceptions;
using FluentValidation.Results;

namespace ECommerce.Application.UseCases.User.Login;

public class LoginUserUseCase : ILoginUserUseCase
{
    private readonly IUserReadOnlyRepository _repository;
    private readonly IAccessTokenGenerator  _accessTokenGenerator;
    private readonly IPasswordEncoder  _passwordEncoder;

    public LoginUserUseCase(IUserReadOnlyRepository repository, IAccessTokenGenerator accessTokenGenerator, IPasswordEncoder passwordEncoder)
    {
        _repository = repository;
        _accessTokenGenerator = accessTokenGenerator;
        _passwordEncoder = passwordEncoder;
    }

    public async Task<UserResponse> Execute(LoginRequest request)
    {
        Validate(request);
        var user =  await _repository.GetUserByEmail(request.Email);
        if (user == null || !_passwordEncoder.Compare(user.Password, request.Password))
        {
            throw new InvalidUserCredentialsException();
        }
        
        return new UserResponse()
        {
            Tokens = [_accessTokenGenerator.GenerateToken(user)]
        };
    }

    private static void Validate(LoginRequest request)
    {
        var validator = new LoginUserValidator();
        var validationResult = validator.Validate(request);

        if (validationResult.Errors.Count != 0)
        {
            throw new ErrorOnValidationException(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
        }
    }
}