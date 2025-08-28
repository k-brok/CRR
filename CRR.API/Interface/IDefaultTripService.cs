using CRR.Shared.Entities;

namespace CRR.API.Interface
{
    public interface IDefaultTripService
    {
        Task<IEnumerable<DefaultTrip>> GetAllAsync();
        Task<DefaultTrip?> GetByIdAsync(Guid id);
        Task<DefaultTrip> CreateAsync(DefaultTrip defaultTrip);
        Task<DefaultTrip?> UpdateAsync(Guid id, DefaultTrip updated);
        Task<bool> DeleteAsync(Guid id);
    }
}
