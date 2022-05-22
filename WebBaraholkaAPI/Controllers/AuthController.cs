using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebBaraholkaAPI.Business.Commands.Interfaces.Auth;
using WebBaraholkaAPI.Core.Responses;
using WebBaraholkaAPI.Filters.Auth;
using WebBaraholkaAPI.Models.Dto.Requests.Auth;

namespace WebBaraholkaAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class AuthController : ControllerBase
{
    private readonly ISignUpCommand _signUpCommand;
    private readonly ISignInCommand _signInCommand;

    public AuthController(
        [FromServices] ISignUpCommand signUpCommand, [FromServices] ISignInCommand signInCommand)
    {
        _signUpCommand = signUpCommand;
        _signInCommand = signInCommand;
    }
    
    [HttpPost("signUp")]
    [SignUpValidationFilter]
    public async Task<CommandResultResponse<string>> SignUp([FromBody] SignUpRequest request)
    {
        return await _signUpCommand.Execute(request);
    }
    
    [HttpPost("signIn")]
    [SignInValidationFilter]
    public async Task<CommandResultResponse<bool>> SignIn([FromBody] SignInRequest request)
    {
        return await _signInCommand.Execute(request);
    }
}