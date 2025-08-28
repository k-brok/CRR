using CRR.Shared.Entities;
using CRR.API.Interface;
using CRR.API.Data;
using Microsoft.EntityFrameworkCore;

namespace CRR.API.Services
{
    public class CarService : ICarService
    {
        private readonly AppDbContext _context;

        public CarService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Car>> GetAllAsync()
        {
            return await _context.Cars
                .ToListAsync();
        }
        public async Task<Car> GetDefaultAsync()
        {
            return await _context.Cars
                .FirstOrDefaultAsync(C => C.Default);
        }

        public async Task<Car?> SetDefaultAsync(Guid id)
        {
            List<Car> AllCars = await _context.Cars.ToListAsync();
            Car UpdatedCar = null;

            foreach (Car car in AllCars)
            {
                if (car.Id == id)
                {
                    car.Default = true;
                    UpdatedCar = car;
                }
                else
                {
                    car.Default = false;
                }
            }
            await _context.SaveChangesAsync();
            return UpdatedCar;
        }

        public async Task<Car?> GetByIdAsync(Guid id)
        {
            return await _context.Cars
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Car> CreateAsync(Car car)
        {
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();
            return car;
        }

        public async Task<Car?> UpdateAsync(Guid id, Car updated)
        {
            var existing = await _context.Cars.FindAsync(id);
            if (existing == null) return null;

            existing.PlateNumber = updated.PlateNumber;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existing = await _context.Cars.FindAsync(id);
            if (existing == null) return false;

            _context.Cars.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Car?> GetByPlatenumberAsync(string Platenumber)
        {
            return await _context.Cars
                .FirstOrDefaultAsync(t => t.PlateNumber == Platenumber);
        }
    }
}
