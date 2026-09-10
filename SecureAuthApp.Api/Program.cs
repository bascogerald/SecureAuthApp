using Microsoft.EntityFrameworkCore;
using SecureAuthApp.Core.Interfaces;
using SecureAuthApp.Infrastracture.Data;
using SecureAuthApp.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

//app to db communication translator
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IAuthService, AuthService>();


//Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy => policy.WithOrigins(
                                    "https://secure-auth-ui-cbc9gbcsbkb5f5dg.westus3-01.azurewebsites.net",
                                    "http://localhost:8081",
                                    "http://localhost:5173",
                                    "http://localhost:3000",
                                    "http://localhost:4173") // Default Vite port
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("AllowReactApp");

app.UseAuthorization();

app.MapControllers();

app.Run();

/* test to activate CI/CD */
