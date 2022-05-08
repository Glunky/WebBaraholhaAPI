using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebBaraholkaAPI.Business.Commands.Interfaces;
using WebBaraholkaAPI.Core.Enums;
using WebBaraholkaAPI.Core.Responses;
using WebBaraholkaAPI.Mappers.Auth.Interfaces;
using WebBaraholkaAPI.Models.Dto.Requests.Auth;

namespace WebBaraholkaAPI.Business.Commands.Implementations;

public class SignUpCommand : ISignUpCommand
{
    private readonly ILogger<SignUpCommand> _logger;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ISignUpToRequestIdentityUserMapper _mapper;

    public SignUpCommand(
         [FromServices] ILogger<SignUpCommand> logger,
         [FromServices] UserManager<IdentityUser> userManager,
         [FromServices] ISignUpToRequestIdentityUserMapper mapper)
    {
        _logger = logger;
        _userManager = userManager;
        _mapper = mapper;
    }
    
    public async Task<CommandResultResponse<string>> Execute(SignUpRequest request)
    {
        IdentityUser newUser = _mapper.Map(request);
        IdentityResult result = await _userManager.CreateAsync(newUser, request.UserInfo.Password);

        if (result.Succeeded)
        {
            _logger.LogInformation("Created new user {UserID}", newUser.Id);
            
            return new CommandResultResponse<string>
            {
                Status = CommandResultStatus.Succeed, 
                Body = newUser.Id
            };
        }
        
        _logger.LogInformation("Failed to create user with id {UserID}", newUser.Id);

        return new CommandResultResponse<string>
        {
            Status = CommandResultStatus.Failed, 
            Body = String.Empty,
            Errors = result.Errors.Select(e => e.Description).ToList()
        };
    }
}