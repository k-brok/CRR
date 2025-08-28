using CRR.Shared.Entities;

namespace CRR.API.Interface
{
    public interface ITripService
    {
        Task<IEnumerable<Trip>> GetAllAsync();
        Task<Trip?> GetByIdAsync(Guid id);
        Task<Trip?> GetLatestAsync();
        Task<Trip> CreateAsync(Trip trip);
        Task<Trip?> UpdateAsync(Guid id, Trip updated);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<Trip>> GetFilteredAsync(
            Guid? fromId = null,
            Guid? toId = null,
            string? remark = null,
            int? departureMileage = null,
            int? arrivalMileage = null,
            DateTime? departureFrom = null,
            DateTime? departureTo = null,
            DateTime? arrivalFrom = null,
            DateTime? arrivalTo = null,
            int? privateMileage = null,
            Guid? carId = null,
            string? carPlateNumber = null
        );
    }
}
