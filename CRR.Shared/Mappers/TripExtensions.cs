using CRR.Shared.DTOs;
using CRR.Shared.Entities;

namespace CRR.Shared.Mappers;

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
            Remark = trip.Remark,
            DepartureMileage = trip.DepartureMileage,
            ArrivalMileage = trip.ArrivalMileage,
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
            FromId = dto.FromId,
            ToId = dto.ToId,
            Departure = dto.Departure,
            Arrival = dto.Arrival,
            Remark = dto.Remark,
            DepartureMileage = dto.DepartureMileage,
            ArrivalMileage = dto.ArrivalMileage,
            Distance = new Distance
            {
                Business = dto.Distance.Business,
                Private = dto.Distance.Private
            }
        };
    }
}
