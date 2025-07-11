using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ConcessionariaAPI.Models {

	[Table("Vehicles")]
	public class Vehicle {
		
		[Key]
		public int Id { get; set; }

		[Required]
		[StringLength(80)]
		public string? Name { get; set; }
		
		[Required]
		[StringLength(20)]
		public string? Color { get; set; }
		public int BrandId { get; set; }

		[JsonIgnore]
		public Brand? brand { get; set; }
	}
}
