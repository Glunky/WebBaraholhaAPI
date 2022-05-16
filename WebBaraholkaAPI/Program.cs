using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using WebBaraholkaAPI.Business.Commands.Implementations;
using WebBaraholkaAPI.Business.Commands.Interfaces;
using WebBaraholkaAPI.Core;
using WebBaraholkaAPI.DbProvider;
using WebBaraholkaAPI.Mappers.Auth.Implementations;
using WebBaraholkaAPI.Mappers.Auth.Interfaces;
using WebBaraholkaAPI.Models.Dto.Requests.Auth;
using WebBaraholkaAPI.Validation.Auth;

// useful refs
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IHostBuilder hostBuilderConfig = builder.Host;
IConfiguration appConfiguration = builder.Configuration;

// configure host
hostBuilderConfig.UseSerilog((hostBuilderContext, loggerConfiguration) =>
    loggerConfiguration
        .WriteTo.Console()
        .WriteTo.Seq("http://localhost:5341")
    );

// services ref
IServiceCollection services = builder.Services;

// services section
services.AddControllers()
    .AddMvcOptions(options => { options.Filters.Add(new AuthorizeFilter()); })
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    })
    .AddFluentValidation(options =>
    {
        options.RegisterValidatorsFromAssembly(Assembly.Load("WebBaraholkaAPI.Validation"), lifetime: ServiceLifetime.Scoped);
        options.ImplicitlyValidateChildProperties = true;
    });
services.AddSwaggerGen();

services.AddDbContext<IdentityContext>(options => 
    options.UseSqlServer(appConfiguration["ConnectionStrings:IdentityConnection"], optionsBuilder => 
        optionsBuilder.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName)));
services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<IdentityContext>();

services.Configure<IdentityOptions>(options =>
{
    options.User.RequireUniqueEmail = true;
});

services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(options =>
{
    options.Events.DisableRedirectForPath(e => e.OnRedirectToLogin, "/api", StatusCodes.Status401Unauthorized);
    options.Events.DisableRedirectForPath(e => e.OnRedirectToAccessDenied, "/api", StatusCodes.Status403Forbidden);
});

services.AddScoped<IValidator<SignUpRequest>, SignUpValidator>();
services.AddScoped<IValidator<SignInRequest>, SignInValidator>();

services.AddScoped<ISignUpToRequestIdentityUserMapper, SignUpToRequestIdentityUserMapper>();
services.AddScoped<IGetWeatherForecastCommand, GetWeatherForecastCommand>();

services.AddScoped<ISignUpCommand, SignUpCommand>();
services.AddScoped<ISignInCommand, SignInCommand>();

// app ref
WebApplication app = builder.Build();

// middleware section
app.UseSwagger();
app.UseSwaggerUI();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

// run app
app.Run();
