using Communication.Requests;
using Communication.Responses;
using ECommerce.Application.UseCases.User.Login;
using ECommerce.Application.UseCases.User.Register;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

[ApiController]
[Route("api/v1/users")]
public class UserController : ControllerBase
{
    
    

    [HttpPost]
    [ProducesResponseType(typeof(UserResponse),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseError),StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseError),StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Register([FromServices] IRegisterUserUseCase  useCase, [FromBody] UserRequest request)
    { 
        var response = await useCase.Execute(request);
        return Ok(response); 
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseError), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login([FromServices]ILoginUserUseCase useCase, [FromBody] LoginRequest request)
    {
        var response = await useCase.Execute(request);
        return Ok(response);
    }
    
    
}