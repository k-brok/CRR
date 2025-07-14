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
            PrivateMileage = trip.PrivateMileage
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
            PrivateMileage = dto.PrivateMileage
        };
    }
    public static Trip ToEntity(this TripDto dto)
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
            PrivateMileage = dto.PrivateMileage
        };
    }
}
