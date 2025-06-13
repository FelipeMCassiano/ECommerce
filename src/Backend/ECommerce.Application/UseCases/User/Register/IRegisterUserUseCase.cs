using Communication.Requests;
using Communication.Responses;

namespace ECommerce.Application.UseCases.User.Register;

public interface IRegisterUserUseCase
{
    Task<UserResponse> Execute(UserRequest request);

}