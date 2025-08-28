using System.ComponentModel.DataAnnotations;

namespace CRR.Shared.Enums
{
	public enum TripType
	{
		[Display(Description = "Zakelijk")]
		Business,
		[Display(Description = "Privé")]
		Private
	}
}