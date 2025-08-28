using Microsoft.EntityFrameworkCore;
using CRR.Shared.Entities;

namespace CRR.API.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options)
			: base(options)
		{
		}

		public DbSet<Trip> Trips { get; set; }
		public DbSet<DefaultTrip> DefaultTrips { get; set; }
		public DbSet<Address> Addresses { get; set; }
		public DbSet<Car> Cars {get; set;}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Trip>()
				.HasOne(e => e.From)
				.WithMany()
				.HasForeignKey(e => e.FromId)
				.IsRequired();

			modelBuilder.Entity<Trip>()
				.HasOne(e => e.To)
				.WithMany()
				.HasForeignKey(e => e.ToId)
				.IsRequired();
		}
	}
}