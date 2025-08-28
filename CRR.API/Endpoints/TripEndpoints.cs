using CRR.Shared.Entities;
using CRR.API.Interface;
using CRR.Shared.DTOs;
using CRR.Shared.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace CRR.API.Endpoints;

public static class TripEndpoints
{
    public static void MapTripEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/trips").WithTags("Trips");

        group.MapGet("/", async (ITripService service) =>
        {
            var trips = await service.GetAllAsync();
            var tripDtos = trips.Select(t => t.ToDto());
            return Results.Ok(tripDtos);
        });

        group.MapGet("/FromYear", async (ITripService service, string year = null) =>
        {
            var trips = await service.GetAllAsync();
            if (year == null)
                year = DateTime.Now.Year.ToString();
            var TripsOfYear = trips.Where(T => T.Departure.Year.ToString() == year);
            var tripDtos = TripsOfYear.Select(t => t.ToDto());
            return Results.Ok(tripDtos);
        });

        group.MapGet("/{id:guid}", async (Guid id, ITripService service) =>
        {
            var trip = await service.GetByIdAsync(id);
            return trip is not null ? Results.Ok(trip.ToDto()) : Results.NotFound();
        });

        group.MapGet("/latest", async (ITripService service) =>
        {
            var trip = await service.GetLatestAsync();
            return trip is not null ? Results.Ok(trip.ToDto()) : Results.NotFound();
        });

        group.MapPost("/", async (CreateTripDto trip, ITripService service) =>
        {
            var created = await service.CreateAsync(trip.ToEntity());
            return Results.Created($"/api/trips/{created.Id}", created);
        });

        group.MapPut("/{id:guid}", async (Guid id, CreateTripDto updated, ITripService service) =>
        {
            var result = await service.UpdateAsync(id, updated.ToEntity());
            return result is not null ? Results.Ok(result) : Results.NotFound();
        });

        group.MapDelete("/{id:guid}", async (Guid id, ITripService service) =>
        {
            var success = await service.DeleteAsync(id);
            return success ? Results.NoContent() : Results.NotFound();
        });

        group.MapGet("/Filter", async (
            Guid? fromId,
            Guid? toId,
            string? remark,
            int? departureMileage,
            int? arrivalMileage,
            DateTime? departureFrom,
            DateTime? departureTo,
            DateTime? arrivalFrom,
            DateTime? arrivalTo,
            int? privateMileage,
            Guid? carId,
            string? carPlateNumber,
            ITripService service) =>
        {
            var trips = await service.GetFilteredAsync(
                fromId, toId, remark,
                departureMileage, arrivalMileage,
                departureFrom, departureTo,
                arrivalFrom, arrivalTo,
                privateMileage, carId, carPlateNumber
            );

            var tripDtos = trips.Select(t => t.ToDto());
            return Results.Ok(tripDtos);
        });

    }
}
