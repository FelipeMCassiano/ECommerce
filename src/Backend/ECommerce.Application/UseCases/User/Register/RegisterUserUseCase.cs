using AutoMapper;
using Communication.Requests;
using ECommerce.Domain.Repositories;
using ECommerce.Domain.Services.Security.Password;
using Exceptions.BaseExceptions;
using Exceptions.MessageExceptions;

namespace ECommerce.Application.UseCases.User.Register;

public class RegisterUserUseCase: IRegisterUserUseCase
{
    private readonly IUserWriteOnlyRepository _repository;
    private readonly IMapper _mapper;
    private readonly IPasswordEncoder _passwordEncoder;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUserUseCase(IUserWriteOnlyRepository repository, IMapper mapper, IPasswordEncoder passwordEncoder, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _mapper = mapper;
        _passwordEncoder = passwordEncoder;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(UserRequest request)
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