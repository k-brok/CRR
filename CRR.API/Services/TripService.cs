using CRR.API.Entities;
using CRR.API.Interface;
using CRR.API.Data;
using Microsoft.EntityFrameworkCore;

namespace CRR.API.Services
{
    public class TripService : ITripService
    {
        private readonly AppDbContext _context;

        public TripService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Trip>> GetAllAsync()
        {
            return await _context.Trips
                .Include(t => t.From)
                .Include(t => t.To)
                .ToListAsync();
        }

        public async Task<Trip?> GetByIdAsync(Guid id)
        {
            return await _context.Trips
                .Include(t => t.From)
                .Include(t => t.To)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Trip> CreateAsync(Trip trip)
        {
            _context.Trips.Add(trip);
            await _context.SaveChangesAsync();
            return trip;
        }

        public async Task<Trip?> UpdateAsync(Guid id, Trip updated)
        {
            var existing = await _context.Trips.FindAsync(id);
            if (existing == null) return null;

            existing.FromId = updated.FromId;
            existing.ToId = updated.ToId;
            existing.Departure = updated.Departure;
            existing.Arrival = updated.Arrival;
            existing.Distance = updated.Distance;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existing = await _context.Trips.FindAsync(id);
            if (existing == null) return false;

            _context.Trips.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
