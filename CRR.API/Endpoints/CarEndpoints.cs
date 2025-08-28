using CRR.Shared.Entities;
using CRR.API.Interface;
using CRR.Shared.DTOs;
using CRR.Shared.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace CRR.API.Endpoints;

public static class CarEndpoints
{
    public static void MapCarEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/cars").WithTags("Cars");
        
        group.MapGet("/", async (ICarService service) =>
        {
            var cars = await service.GetAllAsync();
            var carDtos = cars.Select(t => t.ToDto());
            return Results.Ok(carDtos);
        });

        group.MapGet("/default", async (ICarService service) =>
        {
            var car = await service.GetDefaultAsync();
            var carDto = car.ToDto();
            return Results.Ok(carDto);
        });

        group.MapGet("/{id:guid}", async (Guid id, ICarService service) =>
        {
            var car = await service.GetByIdAsync(id);
            return car is not null ? Results.Ok(car.ToDto()) : Results.NotFound();
        });

        group.MapGet("/PlateNumber/{Platenumber}", async (string Platenumber, ICarService service) =>
        {
            var car = await service.GetByPlatenumberAsync(Platenumber);
            return car is not null ? Results.Ok(car.ToDto()) : Results.NotFound();
        });

        group.MapPost("/", async (CreateCarDto car, ICarService service) =>
        {
            var created = await service.CreateAsync(car.ToEntity());
            return Results.Created($"/api/cars/{created.Id}", created);
        });

        group.MapPut("/{id:guid}", async (Guid id, CreateCarDto updated, ICarService service) =>
        {
            var result = await service.UpdateAsync(id, updated.ToEntity());
            return result is not null ? Results.Ok(result) : Results.NotFound();
        });

        group.MapPut("/default/{id:guid}", async (Guid id, CreateCarDto updated, ICarService service) =>
        {
            var result = await service.SetDefaultAsync(id);
            return result is not null ? Results.Ok(result) : Results.NotFound();
        });

        group.MapDelete("/{id:guid}", async (Guid id, ICarService service) =>
        {
            var success = await service.DeleteAsync(id);
            return success ? Results.NoContent() : Results.NotFound();
        });

    }
}
