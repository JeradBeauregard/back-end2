using Microsoft.AspNetCore.Mvc;
using testapp.Models;

namespace testapp.Interfaces
{
	public interface IItemService
	{

		// create



		// read

		Task<IEnumerable<Item>> GetItems();

		Task<Item> GetItem(int id);

		Task<IEnumerable<ItemWithTypesDto>> GetItemsWithTypes();

		// update


		// delete

		Task<string> DeleteItem(int id);


	}
}
