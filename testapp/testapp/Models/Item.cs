using System.ComponentModel.DataAnnotations;

namespace testapp.Models
{
	public class Item
	{
		[Key]
		public int Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }	

		public int Value { get; set; }

		public virtual List<ItemxType> ItemxType { get; set; }
	}

	public class ItemWithTypesDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public List<string> Types { get; set; }
	}

}
