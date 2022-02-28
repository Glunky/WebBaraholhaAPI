using System;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebBaraholkaAPI.Core.Enums;
using WebBaraholkaAPI.Core.Responses;
using WebBaraholkaAPI.Filters.Auth;
using WebBaraholkaAPI.Models.Dto.Requests.Auth;

namespace WebBaraholkaAPI.Controllers;


[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IValidator<SignUpRequest> _validator;

    public AuthController(
        [FromServices] ILogger<AuthController> logger,
        [FromServices] UserManager<IdentityUser> userManager,
        [FromServices] IValidator<SignUpRequest> validator)
    {
        _logger = logger;
        _userManager = userManager;
        _validator = validator;
    }
    
    [HttpPost("signup")]
    [SignUpValidationFilter]
    public CommandResultResponse<Guid?> SignUp([FromBody] SignUpRequest request)
    {
        return new()
        {
            Status = CommandResultStatus.Succeed, 
            Body = Guid.Empty
        };
        
        // Добавить юзера + фильтр на проверку существует ли такой юзер уже
    }
}