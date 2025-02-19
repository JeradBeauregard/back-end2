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

		// Create
		// add item to inventory
		Task<string> AddToInventory(int userId, int itemId, int quantity);

		//Update

		//Delete
		// Delete inventory row/entry
		Task<string> DeleteInventory(int id);

	}
}
