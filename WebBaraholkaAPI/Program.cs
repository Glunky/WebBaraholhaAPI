// app builder instance
var builder = WebApplication.CreateBuilder(args);

// services ref
IServiceCollection services = builder.Services;

// services section
services.AddControllers();
services.AddSwaggerGen();

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
