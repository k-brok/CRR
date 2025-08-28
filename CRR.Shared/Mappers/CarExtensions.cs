using CRR.Shared.DTOs;
using CRR.Shared.Entities;

namespace CRR.Shared.Mappers;

public static class CarExtensions
{
    public static CarDto ToDto(this Car car)
    {
        return new CarDto
        {
            Id = car.Id,
            PlateNumber = car.PlateNumber,
            Default = car.Default
        };
    }

    public static Car ToEntity(this CreateCarDto dto)
    {
        return new Car
        {
            PlateNumber = dto.PlateNumber,
            Default = dto.Default
        };
    }
    public static Car ToEntity(this CarDto dto)
    {
        return new Car
        {
            Id = dto.Id,
            PlateNumber = dto.PlateNumber,
            Default = dto.Default
        };
    }
}
