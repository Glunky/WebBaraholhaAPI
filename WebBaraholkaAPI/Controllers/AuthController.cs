using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebBaraholkaAPI.Core.Enums;
using WebBaraholkaAPI.Core.Responses;
using WebBaraholkaAPI.Filters.Auth;
using WebBaraholkaAPI.Mappers.Auth.Interfaces;
using WebBaraholkaAPI.Models.Dto.Requests.Auth;

namespace WebBaraholkaAPI.Controllers;


[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ISignUpToRequestIdentityUserMapper _mapper;

    public AuthController(
        [FromServices] ILogger<AuthController> logger,
        [FromServices] UserManager<IdentityUser> userManager,
        [FromServices] ISignUpToRequestIdentityUserMapper mapper)
    {
        _logger = logger;
        _userManager = userManager;
        _mapper = mapper;
    }
    
    [HttpPost("signup")]
    [SignUpValidationFilter]
    public async Task<CommandResultResponse<string>> SignUp([FromBody] SignUpRequest request)
    {
        IdentityUser newUser = _mapper.Map(request);
        IdentityResult result = await _userManager.CreateAsync(newUser, request.UserInfo.Password);

        if (result.Succeeded)
        {
            _logger.LogInformation("Created new user {UserID}", newUser.Id);
            
            return new()
            {
                Status = CommandResultStatus.Succeed, 
                Body = newUser.Id
            };
        }
        
        _logger.LogInformation("Failed to create user with id {UserID}", newUser.Id);

        return new()
        {
            Status = CommandResultStatus.Failed, 
            Body = String.Empty,
            Errors = result.Errors.Select(e => e.Description).ToList()
        };
    }
}