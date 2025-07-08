using CRR.API.Entities;

namespace CRR.API.Interface
{
    public interface IAddressService
    {
        Task<IEnumerable<AddressDto>> GetAllAsync();
        Task<AddressDto?> GetByIdAsync(Guid id);
        Task<AddressDto> CreateAsync(CreateAddressDto address);
        Task<AddressDto?> UpdateAsync(Guid id, CreateAddressDto updatedAddress);
        Task<bool> DeleteAsync(Guid id);
    }
}