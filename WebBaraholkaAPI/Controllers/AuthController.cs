using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
    public async void SignUp([FromBody] SignUpRequest request)
    {
        var validationRes = _validator.Validate(request);
        
            // Добавить юзера + фильтр на проверку существует ли такой юзер уже
        
        _logger.LogInformation("Res:{validationResult}; Name: {consumerName}; Password: {consumerPassword}; Email: {consumerEmail}", 
            validationRes.IsValid, request.ConsumerInformation.Login, request.ConsumerInformation.Password, request.ConsumerInformation.Email);
    }
}