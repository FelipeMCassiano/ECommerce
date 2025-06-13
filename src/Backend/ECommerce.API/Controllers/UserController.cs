using Communication.Requests;
using Communication.Responses;
using ECommerce.Application.UseCases.User.Register;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

[ApiController]
[Route("api/v1/users")]
public class UserController : ControllerBase
{
    
    

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseError),StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseError),StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Register([FromServices] IRegisterUserUseCase  useCase, [FromBody] UserRequest request)
    { 
        await useCase.Execute(request);
        return Ok(); 
    }
    
    
}