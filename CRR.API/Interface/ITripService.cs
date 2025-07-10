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
    }
}
