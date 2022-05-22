using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebBaraholkaAPI.Business.Commands.Interfaces.Auth;
using WebBaraholkaAPI.Core.Enums;
using WebBaraholkaAPI.Core.Responses;
using WebBaraholkaAPI.Models.Dto.Requests.Auth;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace WebBaraholkaAPI.Business.Commands.Implementations.Auth;

public class SignInCommand : ISignInCommand
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly ILogger<SignInCommand> _logger;

    public SignInCommand(
        [FromServices] SignInManager<IdentityUser> signInManager,
        [FromServices] ILogger<SignInCommand> logger)
    {
        _signInManager = signInManager;
        _logger = logger;
    }
    public async Task<CommandResultResponse<bool>> Execute(SignInRequest request)
    {
        SignInResult result = await _signInManager.PasswordSignInAsync(request.UserInfo.Login, request.UserInfo.Password, false, false);

        if (result.Succeeded)
        {
            _logger.LogInformation("User {UserID} successfully authorized", request.UserInfo.Login);
            
            return new CommandResultResponse<bool>
            {
                Status = CommandResultStatus.Succeed, 
                Body = result.Succeeded
            };
        }
        
        _logger.LogInformation("Failed to authorized user {UserID}", request.UserInfo.Login);

        return new CommandResultResponse<bool>
        {
            Status = CommandResultStatus.Failed, 
            Body = result.Succeeded,
        };
    }
}