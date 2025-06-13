using Communication.Requests;

namespace ECommerce.Application.UseCases.User.Register;

public interface IRegisterUserUseCase
{
    Task Execute(UserRequest request);

}