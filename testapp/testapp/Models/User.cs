using System.ComponentModel.DataAnnotations;

namespace testapp.Models

{
	public class User
	{
		[Key]
		public int Id { get; set; }

		public string Username { get; set; }

		public string Password { get; set; }

		public int	InventorySpace {  get; set; }

		public int	SolShards { get; set; }
	}

	public class CreateUserDto
	{
		public int UserId { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public int InventorySpace { get; set; }
		public int SolShards { get; set; }
	}
}
