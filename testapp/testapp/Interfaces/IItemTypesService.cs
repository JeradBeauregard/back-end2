using Microsoft.AspNetCore.Mvc;
using testapp.Models;

namespace testapp.Interfaces
{
	public interface IItemTypesService
	{

		// Create
		Task<ItemType> CreateItemType(string type);

		// Read
		Task<IEnumerable<ItemType>> GetItemTypes();
		// get all types for an item
		Task<IEnumerable<ItemTypeDto>> GetTypesForItem(int itemId);

		// Update

		// Delete
	}
}
