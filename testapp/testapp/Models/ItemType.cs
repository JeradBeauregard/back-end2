using System.ComponentModel.DataAnnotations;

namespace testapp.Models
{
	public class ItemType
	{
		[Key]
		public int Id { get; set; }

		public string Type {  get; set; }

		// Back-reference for Many-to-Many Relationship
		public virtual List<ItemxType> ItemXTypes { get; set; }
	}
}
