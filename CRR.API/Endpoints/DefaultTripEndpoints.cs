using CRR.Shared.Entities;
using CRR.API.Interface;
using CRR.Shared.DTOs;
using CRR.Shared.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace CRR.API.Endpoints;

public static class DefaultTripEndpoints
{
    public static void MapDefaultTripEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/DefaultTrips");

        group.MapGet("/", async (IDefaultTripService service) =>
        {
            var DefaultTrips = await service.GetAllAsync();
            var DefaultTripDtos = DefaultTrips.Select(t => t.ToDto());
            return Results.Ok(DefaultTripDtos);
        });

        group.MapGet("/{id:guid}", async (Guid id, IDefaultTripService service) =>
        {
            var DefaultTrip = await service.GetByIdAsync(id);
            return DefaultTrip is not null ? Results.Ok(DefaultTrip.ToDto()) : Results.NotFound();
        });

        group.MapGet("/FromAddresses", async (Guid from, Guid to, IDefaultTripService service) =>
        {
            var DefaultTrip = await service.GetByAddressesAsync(from,to);
            return DefaultTrip is not null ? Results.Ok(DefaultTrip.ToDto()) : Results.NotFound();
        });

        group.MapPost("/", async (CreateDefaultTripDto DefaultTrip, IDefaultTripService service) =>
        {
            var created = await service.CreateAsync(DefaultTrip.ToEntity());
            return Results.Created($"/api/DefaultTrips/{created.Id}", created);
        });

        group.MapPost("/Import", async (List<CreateDefaultTripDto> DefaultTripList, IDefaultTripService service) =>
        {
            foreach (CreateDefaultTripDto DefaultTrip in DefaultTripList)
            {
                await service.CreateAsync(DefaultTrip.ToEntity());
            }
            return Results.Created();
        });


        group.MapPut("/{id:guid}", async (Guid id, CreateDefaultTripDto updated, IDefaultTripService service) =>
        {
            var result = await service.UpdateAsync(id, updated.ToEntity());
            return result is not null ? Results.Ok(result) : Results.NotFound();
        });

        group.MapDelete("/{id:guid}", async (Guid id, IDefaultTripService service) =>
        {
            var success = await service.DeleteAsync(id);
            return success ? Results.NoContent() : Results.NotFound();
        });

    }
}
