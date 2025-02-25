﻿using Microsoft.AspNetCore.Mvc;
using testapp.Models;
using testapp.Services;

namespace testapp.Interfaces
{
	public interface IItemService
	{

		// create
		Task<CreateItemDto> CreateItem(string name, string description, int value);


		// read

		Task<IEnumerable<UserByItemDto>> ListUsersByItem(int itemId);

		Task<IEnumerable<Item>> GetItems();

		Task<Item> GetItem(int id);

		Task<IEnumerable<ItemWithTypesDto>> GetItemsWithTypes();

		// get all items per a type
		Task<IEnumerable<ItemsForTypeDto>> GetItemsForType(int typeId);

		// update

		//link item to item type
		Task<string> LinkItemToType(int itemId, int typeId);

		// unlink item from item type

		Task<string> UnlinkItemToType(int itemId, int typeId);

		// edit existing item

		Task<CreateItemDto> EditItem(int id, string name, string description, int value);




		// delete

		Task<string> DeleteItem(int id);


	}
}
