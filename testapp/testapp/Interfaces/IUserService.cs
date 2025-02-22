using testapp.Models;

namespace testapp.Interfaces
{
	public interface IUserService
	{

		// update inventory space

		//Task<IEnumerable<InventoryDto>> ListUserInventory(int userId);

		Task<string> updateUserSpace(int userId, int spaceChange);

		Task<IEnumerable<User>> GetUsers();

		// create user
		Task<CreateUserDto> PostUser(string username, string password);

	}
}
