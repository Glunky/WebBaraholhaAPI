using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using WebBaraholkaAPI.Business.Commands.Implementations;
using WebBaraholkaAPI.Business.Commands.Interfaces;


// app builder ref
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
// host builder ref
IHostBuilder hostBuilderConfig = builder.Host;

// configure host
hostBuilderConfig.UseSerilog((hostBuilderContext, loggerConfiguration) =>
    loggerConfiguration
        .WriteTo.Console()
        .WriteTo.Seq("http://localhost:5341")
    );

// services ref
IServiceCollection services = builder.Services;

// services section
services.AddControllers();
services.AddSwaggerGen();
services.AddTransient<IGetWeatherForecastCommand, GetWeatherForecastCommand>();

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
