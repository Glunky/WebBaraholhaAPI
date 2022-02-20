using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using WebBaraholkaAPI.Business.Commands.Implementations;
using WebBaraholkaAPI.Business.Commands.Interfaces;


// app builder instance
var builder = WebApplication.CreateBuilder(args);

// services ref
IServiceCollection services = builder.Services;

// services section
services.AddControllers();
services.AddSwaggerGen();
services.AddTransient<IGetWeatherForecastCommand, GetWeatherForecastCommand>();

// app instance
var app = builder.Build();

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
