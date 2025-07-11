using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ConcessionariaAPI.Models {

	[Table("Brands")]
	public class Brand {

		[Key]
		public int Id { get; set; }

		[Required]
		[StringLength(80)]
		public string? Name { get; set; }

		public ICollection<Vehicle>? Vehicles { get; set; }

		public Brand() {
			Vehicles = new Collection<Vehicle>();
		}
	}
}
