using CRR.Shared.Entities;

namespace CRR.API.Interface
{
    public interface ICarService
    {
        Task<IEnumerable<Car>> GetAllAsync();
        Task<Car?> GetByIdAsync(Guid id);
        Task<Car?> GetByPlatenumberAsync(string Platenumber);
        Task<Car> CreateAsync(Car car);
        Task<Car?> UpdateAsync(Guid id, Car updated);
        Task<bool> DeleteAsync(Guid id);
        Task<Car> GetDefaultAsync();
        Task<Car?> SetDefaultAsync(Guid id);
    }
}
