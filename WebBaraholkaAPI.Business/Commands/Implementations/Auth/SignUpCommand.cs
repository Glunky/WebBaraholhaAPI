using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebBaraholkaAPI.Business.Commands.Interfaces.Auth;
using WebBaraholkaAPI.Core.Enums;
using WebBaraholkaAPI.Core.Responses;
using WebBaraholkaAPI.Mappers.Auth.Interfaces;
using WebBaraholkaAPI.Models.Db;
using WebBaraholkaAPI.Models.Dto.Requests.Auth;

namespace WebBaraholkaAPI.Business.Commands.Implementations.Auth;

public class SignUpCommand : ISignUpCommand
{
    private readonly ILogger<SignUpCommand> _logger;
    private readonly UserManager<DbApplicationUser> _userManager;
    private readonly ISignUpToRequestIdentityUserMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SignUpCommand(
         [FromServices] ILogger<SignUpCommand> logger,
         [FromServices] UserManager<DbApplicationUser> userManager,
         [FromServices] ISignUpToRequestIdentityUserMapper mapper,
         [FromServices] IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _userManager = userManager;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<CommandResultResponse<string>> Execute(SignUpRequest request)
    {
        DbApplicationUser newUser = _mapper.Map(request);
        IdentityResult result = await _userManager.CreateAsync(newUser, request.UserInfo.Password);

        if (result.Succeeded)
        {
            _logger.LogInformation("Created new user {UserID}", newUser.Id);
            _httpContextAccessor.HttpContext.Response.StatusCode = (int) HttpStatusCode.Created;
            
            return new CommandResultResponse<string>
            {
                Status = CommandResultStatus.Succeed, 
                Body = newUser.Id
            };
        }
        
        _logger.LogInformation("Failed to create user with id {UserID}", newUser.Id);
        _httpContextAccessor.HttpContext.Response.StatusCode = (int) HttpStatusCode.Conflict;
        
        return new CommandResultResponse<string>
        {
            Status = CommandResultStatus.Failed, 
            Body = String.Empty,
            Errors = result.Errors.Select(e => e.Description).ToList()
        };
    }
}