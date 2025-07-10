using CRR.Shared.DTOs;
using CRR.Shared.Entities;
using CRR.Shared.Enums;
namespace CRR.Shared.Mappers;

public static class AddressExtensions
{
    public static AddressDto ToDto(this Address Address)
    {
        return new AddressDto
        {
            Id = Address.Id,
            Name = Address.Name,
            Street = Address.Street,
            Number = Address.Number,
            ZipCode = Address.ZipCode,
            Type = Address.Type.ToString()
        };
    }

    public static Address ToEntity(this CreateAddressDto dto)
    {
        TripType Converttype = TripType.Private;

        Enum.TryParse(dto.Type, true, out Converttype);

        return new Address
        {
            Name = dto.Name,
            Street = dto.Street,
            Number = dto.Number,
            ZipCode = dto.ZipCode,
            Type = Converttype
        };
    }

    public static Address ToEntity(this AddressDto dto)
    {
        TripType Converttype = TripType.Private;

        Enum.TryParse(dto.Type, true, out Converttype);

        return new Address
        {
            Name = dto.Name,
            Street = dto.Street,
            Number = dto.Number,
            ZipCode = dto.ZipCode,
            Type = Converttype
        };
    }
}
