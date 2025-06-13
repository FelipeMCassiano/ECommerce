using Communication.Requests;
using Communication.Responses;

namespace ECommerce.Application.UseCases.User.Login;

public interface ILoginUserUseCase
{
    Task<UserResponse> Execute(LoginRequest request);
    
}