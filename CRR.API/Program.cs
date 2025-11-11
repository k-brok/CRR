using CRR.API.Data;
using Microsoft.EntityFrameworkCore;
using CRR.API.Interface;
using CRR.API.Services;
using CRR.API.Endpoints;
using Microsoft.AspNetCore.HttpOverrides;

namespace CRR.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Services
        builder.Services.AddAuthorization();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddScoped<IAddressService, AddressService>();
        builder.Services.AddScoped<ITripService, TripService>();
        builder.Services.AddScoped<IDefaultTripService, DefaultTripService>();
        builder.Services.AddScoped<ICarService, CarService>();

        var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>() ?? Array.Empty<string>();
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", policy =>
            {
                policy.WithOrigins(allowedOrigins)
                      .AllowAnyMethod()
                      .AllowAnyHeader();
                // .AllowCredentials(); // alleen als je cookies/auth via browser gebruikt
            });
        });

        var app = builder.Build();

        // DB migratie
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Database.Migrate();
        }

        // ===== Middleware volgorde =====
        // 1) Forwarded headers zo vroeg mogelijk
        var fwd = new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
        };
        // Als je achter een externe proxy zit (NPM/NGINX), deze leegmaken:
        fwd.KnownNetworks.Clear();
        fwd.KnownProxies.Clear();
        app.UseForwardedHeaders(fwd);

        // 2) Swagger (onder /api)
        app.UseSwagger(c =>
        {
            // PATTERN, niet hardcoded pad:
            c.RouteTemplate = "api/swagger/{documentName}/swagger.json";
        });
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/api/swagger/v1/swagger.json", "CRR API v1");
            c.RoutePrefix = "api/swagger"; // UI op /api/swagger
        });

        // 3) Overige middleware
        app.UseHttpsRedirection();
        app.UseCors("CorsPolicy");
        app.UseAuthorization();

        // Endpoints
        app.MapAddressEndpoints();
        app.MapTripEndpoints();
        app.MapDefaultTripEndpoints();
        app.MapCarEndpoints();

        app.Run();
    }
}
