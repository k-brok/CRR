using CRR.Shared.Entities;
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
                .Include(t => t.Car)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Trip?> GetLatestAsync()
        {
            return await _context.Trips
                .Include(t => t.From)
                .Include(t => t.To)
                .Include(t => t.Car)
                .OrderByDescending(t => t.Arrival)
                .FirstOrDefaultAsync();
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
            existing.Remark = updated.Remark;
            existing.DepartureMileage = updated.DepartureMileage;
            existing.ArrivalMileage = updated.ArrivalMileage;
            existing.Departure = updated.Departure;
            existing.Arrival = updated.Arrival;
            existing.PrivateMileage = updated.PrivateMileage;

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

        public async Task<IEnumerable<Trip>> GetFilteredAsync(
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
        )
        {
            IQueryable<Trip> query = _context.Trips
                .Include(t => t.From)
                .Include(t => t.To)
                .Include(t => t.Car);

            if (fromId.HasValue)
                query = query.Where(t => t.FromId == fromId.Value);

            if (toId.HasValue)
                query = query.Where(t => t.ToId == toId.Value);

            if (!string.IsNullOrWhiteSpace(remark))
                query = query.Where(t => t.Remark.Contains(remark));

            if (departureMileage.HasValue)
                query = query.Where(t => t.DepartureMileage == departureMileage.Value);

            if (arrivalMileage.HasValue)
                query = query.Where(t => t.ArrivalMileage == arrivalMileage.Value);

            if (departureFrom.HasValue)
                query = query.Where(t => t.Departure >= departureFrom.Value);

            if (departureTo.HasValue)
                query = query.Where(t => t.Departure <= departureTo.Value);

            if (arrivalFrom.HasValue)
                query = query.Where(t => t.Arrival >= arrivalFrom.Value);

            if (arrivalTo.HasValue)
                query = query.Where(t => t.Arrival <= arrivalTo.Value);

            if (privateMileage.HasValue)
                query = query.Where(t => t.PrivateMileage == privateMileage.Value);

            if (carId.HasValue)
                query = query.Where(t => t.CarId == carId.Value);

            if (!string.IsNullOrWhiteSpace(carPlateNumber))
                query = query.Where(t => t.Car.PlateNumber == carPlateNumber);

            return await query.ToListAsync();
        }
    }
}
