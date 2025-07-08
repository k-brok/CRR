using CRR.API.Entities;

namespace CRR.API.Interface
{
    public interface ITripService
    {
        Task<IEnumerable<TripDto>> GetAllAsync();
        Task<TripDto?> GetByIdAsync(Guid id);
        Task<TripDto> CreateAsync(CresteTripDto trip);
        Task<TripDto?> UpdateAsync(Guid id, CresteTripDto updated);
        Task<bool> DeleteAsync(Guid id);
    }
}
