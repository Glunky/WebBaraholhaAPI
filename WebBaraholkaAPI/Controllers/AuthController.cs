using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebBaraholkaAPI.Business.Commands.Interfaces;
using WebBaraholkaAPI.Core.Responses;
using WebBaraholkaAPI.Filters.Auth;
using WebBaraholkaAPI.Models.Dto.Requests.Auth;

namespace WebBaraholkaAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ISignUpCommand _signUpCommand;

    public AuthController(
        [FromServices] ISignUpCommand signUpCommand) => _signUpCommand = signUpCommand;
    
    [HttpPost("signup")]
    [SignUpValidationFilter]
    public async Task<CommandResultResponse<string>> SignUp([FromBody] SignUpRequest request)
    {
        return await _signUpCommand.Execute(request);
    }
}