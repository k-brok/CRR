using CRR.Shared.Enums;

namespace CRR.Shared.Entities{
	public class DefaultTrip
	{
		public TripType Type { get; set; }
		public Guid Id { get; set; }
		public Address From { get; set; }
		public Guid FromId { get; set; }
		public Address To { get; set; }
		public Guid ToId { get; set; }
		public int DefaultMileage { get; set; }
		public int PrivateMileage { get; set; }
		public TimeSpan TripTime { get; set; }
		public bool Deleted {get; set;}
	}
}