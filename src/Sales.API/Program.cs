using Sales.API.Configurations;
using SharedLib.Tokens.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.RegisterAllDependencies();
builder.AddDocumentationConfig();
builder.Services.AddJwtConfiguration(builder.Configuration);
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthConfiguration();

app.MapControllers();

app.Run();
