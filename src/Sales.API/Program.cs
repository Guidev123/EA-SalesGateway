using SharedLib.Tokens.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddJwtConfiguration(builder.Configuration);
builder.Services.AddAuthorization();

var app = builder.Build();

app.MapOpenApi();

app.UseHttpsRedirection();

app.UseAuthConfiguration();

app.MapControllers();

app.Run();
