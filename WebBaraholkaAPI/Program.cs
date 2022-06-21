
using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Serilog;
using WebBaraholkaAPI.Business.Commands.Implementations.Auth;
using WebBaraholkaAPI.Business.Commands.Implementations.FoodProducts;
using WebBaraholkaAPI.Business.Commands.Interfaces.Auth;
using WebBaraholkaAPI.Business.Commands.Interfaces.FoodProducts;
using WebBaraholkaAPI.Core;
using WebBaraholkaAPI.Data;
using WebBaraholkaAPI.Data.Implementations;
using WebBaraholkaAPI.Data.Interfaces;
using WebBaraholkaAPI.DbProvider;
using WebBaraholkaAPI.Mappers.Auth.Implementations;
using WebBaraholkaAPI.Mappers.Auth.Interfaces;
using WebBaraholkaAPI.Mappers.FoodProducts.Implementations;
using WebBaraholkaAPI.Mappers.FoodProducts.Interfaces;
using WebBaraholkaAPI.Models.Db;
using WebBaraholkaAPI.Models.Dto.Requests.Auth;
using WebBaraholkaAPI.Models.Dto.Requests.FoodProducts;
using WebBaraholkaAPI.Validation.Auth;
using WebBaraholkaAPI.Validation.FoodProducts;

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

void AddNativeServices()
{
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
    services.AddDbContext<DataContext>(options => 
        options.UseSqlServer(appConfiguration["ConnectionStrings:WebBaraholkaAPIConnection"], optionsBuilder =>
        {
            optionsBuilder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
            optionsBuilder.MigrationsAssembly(typeof(DataContext).Assembly.FullName);
        }));
            
    services.AddIdentity<DbApplicationUser, IdentityRole>().AddEntityFrameworkStores<DataContext>();

    services.Configure<IdentityOptions>(options =>
    {
        options.User.RequireUniqueEmail = false;
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
}

void AddDbServices()
{
    services.AddScoped<IDataProvider, DataContext>();
    services.AddScoped<IFoodProductsRepository, FoodProductsRepository>();
}

void AddValidationServices()
{
    services.AddSingleton<IValidator<SignUpRequest>, SignUpValidator>();
    services.AddSingleton<IValidator<SignInRequest>, SignInValidator>();
    services.AddSingleton<IValidator<AddNewConsumedFoodRecordRequest>, AddNewConsumedFoodRecordValidator>();
}

void AddMapperServices()
{
    services.AddSingleton<ISignUpToRequestIdentityUserMapper, SignUpToRequestIdentityUserMapper>();
    services.AddSingleton<IConsumedFoodProductToDbModelMapper, ConsumedFoodProductToDbModelMapper>();
    services.AddSingleton<IDbFoodProductToFoodProductResponseMapper, DbFoodProductToFoodProductResponseMapper>();
    services.AddSingleton<IDbFoodCategoryToFoodCategoryResponseMapper, DbFoodCategoryToFoodCategoryResponseMapper>();
}

void AddCommandsServices()
{
    services.AddScoped<ISignUpCommand, SignUpCommand>();
    services.AddScoped<ISignInCommand, SignInCommand>();
    services.AddScoped<IGetFoodProductsCommand, GetFoodProductsCommand>();
    services.AddScoped<IGetFoodCategoriesCommand, GetFoodCategoriesCommand>();
    services.AddScoped<IAddNewConsumedFoodRecordCommand, AddNewConsumedFoodRecordCommand>();
    services.AddScoped<IGetConsumedFoodProductsHistory, GetConsumedFoodProductsHistoryCommand>();
}

AddNativeServices();
AddDbServices();
AddValidationServices();
AddMapperServices();
AddCommandsServices();

// app ref
WebApplication app = builder.Build();
IApplicationBuilder appBuilder = app;
IServiceProvider serviceProvider = appBuilder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope().ServiceProvider;

serviceProvider.GetService<DataContext>()!.Database.Migrate();

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