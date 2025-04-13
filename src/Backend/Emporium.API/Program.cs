using Carter;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Emporium.Application;
using Emporium.Infrastructure;

namespace Emporium.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var jwtSettings = builder.Configuration.GetSection("Jwt");
        var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);

        _ = builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["Issuer"],
                ValidAudience = jwtSettings["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };
        });

        _ = builder.Services.AddSwaggerGen();

        // Add services to the container.
        _ = builder.Services.AddAuthorization();

        // Endpoint Modules
        _ = builder.Services.AddCarter();

        // Clean Architecture
        _ = builder.Services.AddApplication();
        _ = builder.Services.AddInfrastructure(builder.Configuration);

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            _ = app.UseSwagger();
            _ = app.UseSwaggerUI();
        }

        _ = app.UseHttpsRedirection();

        _ = app.UseAuthentication();
        _ = app.UseAuthorization();

        // VER https://github.com/reactiveui/refit

        _ = app.MapCarter();

        app.Run();
    }
}
