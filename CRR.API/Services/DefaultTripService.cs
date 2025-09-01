using CRR.Shared.Entities;
using CRR.API.Interface;
using CRR.API.Data;
using Microsoft.EntityFrameworkCore;

namespace CRR.API.Services
{
    public class DefaultTripService : IDefaultTripService
    {
        private readonly AppDbContext _context;

        public DefaultTripService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DefaultTrip>> GetAllAsync()
        {
            return await _context.DefaultTrips
                .Include(t => t.From)
                .Include(t => t.To)
                .ToListAsync();
        }

        public async Task<DefaultTrip?> GetByIdAsync(Guid id)
        {
            return await _context.DefaultTrips
                .Include(t => t.From)
                .Include(t => t.To)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<DefaultTrip?> GetByAddressesAsync(Guid from, Guid to)
        {
            return await _context.DefaultTrips
                .Include(t => t.From)
                .Include(t => t.To)
                .FirstOrDefaultAsync(t => t.FromId == from && t.ToId == to);
        }

        public async Task<DefaultTrip> CreateAsync(DefaultTrip defaultTrip)
        {
            _context.DefaultTrips.Add(defaultTrip);
            await _context.SaveChangesAsync();
            return defaultTrip;
        }

        public async Task<DefaultTrip?> UpdateAsync(Guid id, DefaultTrip updated)
        {
            var existing = await _context.DefaultTrips.FindAsync(id);
            if (existing == null) return null;

            existing.FromId = updated.FromId;
            existing.ToId = updated.ToId;
            existing.Type = updated.Type;
            existing.DefaultMileage = updated.DefaultMileage;
            existing.PrivateMileage = updated.PrivateMileage;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existing = await _context.DefaultTrips.FindAsync(id);
            if (existing == null) return false;

            _context.DefaultTrips.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
