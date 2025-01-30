using Sales.API.Configurations;
using Sales.API.Middlewares;
using SharedLib.Tokens.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.AddSetupConfig();
builder.AddCorsConfig();
builder.RegisterAllDependencies();
builder.Services.AddJwtConfiguration(builder.Configuration);

var app = builder.Build();
app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseSecurity();

app.Run();