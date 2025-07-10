using CRR.Shared.Entities;
using CRR.API.Interface;
using CRR.Shared.DTOs;
using CRR.Shared.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace CRR.API.Endpoints
{
    public static class AddressEndpoints
    {
        public static void MapAddressEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/addresses");

            group.MapGet("/", async (IAddressService service) =>
            {
                var all = await service.GetAllAsync();
                var AddressDtos = all.Select(t => t.ToDto());
                return Results.Ok(AddressDtos);
            });

            group.MapGet("/{id:guid}", async (Guid id, IAddressService service) =>
            {
                var address = await service.GetByIdAsync(id);
                return address is not null ? Results.Ok(address) : Results.NotFound();
            });

            group.MapPost("/", async (CreateAddressDto address, IAddressService service) =>
            {
                var created = await service.CreateAsync(address.ToEntity());
                return Results.Created($"/api/addresses/{created.Id}", created);
            });

            group.MapPut("/{id:guid}", async (Guid id, CreateAddressDto updated, IAddressService service) =>
            {
                var result = await service.UpdateAsync(id, updated.ToEntity());
                return result is not null ? Results.Ok(result) : Results.NotFound();
            });

            group.MapDelete("/{id:guid}", async (Guid id, IAddressService service) =>
            {
                var success = await service.DeleteAsync(id);
                return success ? Results.NoContent() : Results.NotFound();
            });
        }
    }
}
