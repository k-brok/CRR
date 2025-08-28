using CRR.Shared.DTOs;
using CRR.Shared.Entities;
using CRR.Shared.Enums;
namespace CRR.Shared.Mappers;

public static class DefaultTripExtensions
{
    public static DefaultTripDto ToDto(this DefaultTrip DefaultTrip)
    {
        return new DefaultTripDto
        {
            Id = DefaultTrip.Id,
            FromId = DefaultTrip.FromId,
            ToId = DefaultTrip.ToId,
            DefaultMileage = DefaultTrip.DefaultMileage,
            PrivateMileage = DefaultTrip.PrivateMileage,
            Type = DefaultTrip.Type.ToString()
        };
    }

    public static DefaultTrip ToEntity(this CreateDefaultTripDto dto)
    {
        TripType Converttype = TripType.Private;

        Enum.TryParse(dto.Type, true, out Converttype);

        return new DefaultTrip
        {
            FromId = dto.FromId,
            ToId = dto.ToId,
            DefaultMileage = dto.DefaultMileage,
            PrivateMileage = dto.PrivateMileage,
            Type = Converttype
        };
    }

    public static DefaultTrip ToEntity(this DefaultTripDto dto)
    {
        TripType Converttype = TripType.Private;

        Enum.TryParse(dto.Type, true, out Converttype);

        return new DefaultTrip
        {
            Id = dto.Id,
            FromId = dto.FromId,
            ToId = dto.ToId,
            DefaultMileage = dto.DefaultMileage,
            PrivateMileage = dto.PrivateMileage,
            Type = Converttype
        };
    }
}
