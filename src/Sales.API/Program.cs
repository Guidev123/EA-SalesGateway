using Microsoft.Extensions.Options;
using Sales.API.Configurations;
using Sales.API.Extensions;
using Sales.API.Services.Cart;
using SharedLib.Tokens.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.RegisterAllDependencies();
builder.Services.AddJwtConfiguration(builder.Configuration);
builder.Services.AddAuthorization();

var app = builder.Build();

app.MapOpenApi();

app.UseHttpsRedirection();

app.UseAuthConfiguration();

app.MapControllers();

app.Run();
