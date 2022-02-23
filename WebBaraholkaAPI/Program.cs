using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using WebBaraholkaAPI.DbProvider;
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

services.AddScoped<IValidator<SignUpRequest>, SignUpValidator>();

// app ref
WebApplication app = builder.Build();

// middleware section
app.UseSwagger();
app.UseSwaggerUI();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

// run app
app.Run();
