using CRR.Shared.Entities;

namespace CRR.API.Interface
{
    public interface IAddressService
    {
        Task<IEnumerable<Address>> GetAllAsync();
        Task<Address?> GetByIdAsync(Guid id);
        Task<Address?> GetByZIPAsync(string ZipCode,string Number);
        Task<Address> CreateAsync(Address address);
        Task<Address?> UpdateAsync(Guid id, Address updatedAddress);
        Task<bool> DeleteAsync(Guid id);
    }
}