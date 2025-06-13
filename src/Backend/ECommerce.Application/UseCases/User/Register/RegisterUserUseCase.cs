using AutoMapper;
using Communication.Requests;
using Communication.Responses;
using ECommerce.Domain.Repositories;
using ECommerce.Domain.Security.Cryptography;
using ECommerce.Domain.Security.Tokens;
using Exceptions.BaseExceptions;
using Exceptions.MessageExceptions;

namespace ECommerce.Application.UseCases.User.Register;

public class RegisterUserUseCase: IRegisterUserUseCase
{
    private readonly IUserWriteOnlyRepository _repository;
    private readonly IMapper _mapper;
    private readonly IPasswordEncoder _passwordEncoder;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAccessTokenGenerator _accessTokenGenerator;

    public RegisterUserUseCase(IUserWriteOnlyRepository repository, IMapper mapper, IPasswordEncoder passwordEncoder, IUnitOfWork unitOfWork, IAccessTokenGenerator accessTokenGenerator)
    {
        _repository = repository;
        _mapper = mapper;
        _passwordEncoder = passwordEncoder;
        _unitOfWork = unitOfWork;
        _accessTokenGenerator = accessTokenGenerator;
    }

    public async Task<UserResponse> Execute(UserRequest request)
    {
        Validate(request);
        var exists = await _repository.ExistsUserWithEmailAsync(request.Email);
        Console.WriteLine(exists);
        if (exists)
        {
            throw new ConflictEntityException(UserExceptionMessages.UserWithEmailAlreadyExists);
        }
        
        var user = _mapper.Map<Domain.Entities.User>(request);
        
        user.Password = _passwordEncoder.Encode(request.Password);
        
        await _repository.AddAsync(user);
        
        await _unitOfWork.CommitAsync();

        return new UserResponse()
        {
            Tokens = [_accessTokenGenerator.GenerateToken(user)]
        };
    }

    private static void Validate(UserRequest request)
    {
        var validator = new RegisterUserValidator();
        var result = validator.Validate(request);

        if (result.Errors.Count != 0)
        {
            throw new ErrorOnValidationException(result.Errors.Select(e =>e.ErrorMessage).ToList());
        }
        
    }
}