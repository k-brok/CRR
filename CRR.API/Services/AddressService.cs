using CRR.API.Entities;
using CRR.API.Interface;
using CRR.API.Data;
using Microsoft.EntityFrameworkCore;

namespace CRR.API.Services
{
    public class AddressService : IAddressService
    {
        private readonly AppDbContext _context;

        public AddressService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Address>> GetAllAsync()
        {
            return await _context.Set<Address>().ToListAsync();
        }

        public async Task<Address?> GetByIdAsync(Guid id)
        {
            return await _context.Set<Address>().FindAsync(id);
        }

        public async Task<Address> CreateAsync(Address address)
        {
            _context.Set<Address>().Add(address);
            await _context.SaveChangesAsync();
            return address;
        }

        public async Task<Address?> UpdateAsync(Guid id, Address updatedAddress)
        {
            var existing = await _context.Set<Address>().FindAsync(id);
            if (existing == null) return null;

            existing.Name = updatedAddress.Name;
            existing.street = updatedAddress.street;
            existing.Number = updatedAddress.Number;
            existing.ZipCode = updatedAddress.ZipCode;
            existing.Type = updatedAddress.Type;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existing = await _context.Set<Address>().FindAsync(id);
            if (existing == null) return false;

            _context.Set<Address>().Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
