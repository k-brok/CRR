using CRR.API.DTOs;
using CRR.API.Entities;

namespace CRR.API.Mappers;

public static class TripExtensions
{
    public static TripDto ToDto(this Trip trip)
    {
        return new TripDto
        {
            Id = trip.Id,
            FromId = trip.FromId,
            ToId = trip.ToId,
            Departure = trip.Departure,
            Arrival = trip.Arrival,
            Distance = new DistanceDto
            {
                Business = trip.Distance.Business,
                Private = trip.Distance.Private
            }
        };
    }

    public static Trip ToEntity(this CreateTripDto dto)
    {
        return new Trip
        {
            Id = dto.Id,
            FromId = dto.FromId,
            ToId = dto.ToId,
            Departure = dto.Departure,
            Arrival = dto.Arrival,
            Distance = new Distance
            {
                Business = dto.Distance.Business,
                Private = dto.Distance.Private
            }
        };
    }
}
