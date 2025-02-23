using Microsoft.AspNetCore.Mvc;
using testapp.Models;

namespace testapp.Interfaces
{
	public interface IInventoryService
	{
		// Read

		// list user inventory
		//Task<IEnumerable<CategoryDto>> ListCategories();
		Task<IEnumerable<InventoryDto>> ListUserInventory(int userId);
		Task<IEnumerable<InventoryDto>> ListInventories();
		Task<InventoryDto> ListInventory(int id);

		// Create
		// add item to inventory
		Task<string> AddToInventory(int userId, int itemId, int quantity);

		//Update
		Task<InventoryDto>EditInventory(int id, int userId, int itemId, int quantity);
		

			//Delete
			// Delete inventory row/entry
			Task<string> DeleteInventory(int id);

	}
}
