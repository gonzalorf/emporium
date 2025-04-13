
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

        // Add services to the container.
        _ = builder.Services.AddAuthorization();

        // Endpoint Modules
        _ = builder.Services.AddCarter();

        // MediatR - CQRS
        _ = builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        //_ = builder.Services.AddOpenApi();

        builder.Services.AddSwaggerGen();

        // Clean Architecture
        _ = builder.Services.AddApplication();
        _ = builder.Services.AddInfrastructure(builder.Configuration);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            //app.MapOpenApi();
        }
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        var summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        // VER https://github.com/reactiveui/refit

        //app.MapGet("/test", (HttpContext httpContext) =>
        //{
        //    var forecast = Enumerable.Range(1, 5).Select(index =>
        //        new WeatherForecast
        //        {
        //            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
        //            TemperatureC = Random.Shared.Next(-20, 55),
        //            Summary = summaries[Random.Shared.Next(summaries.Length)]
        //        })
        //        .ToArray();
        //    return forecast;
        //})
        //.WithName("GetWeatherForecast");

        app.MapCarter();

        app.Run();
    }
}
