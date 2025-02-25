﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using testapp.Interfaces;
using testapp.Models;

namespace testapp.Controllers
{
	public class ItemPageController : Controller
	{
		// Dependancy Injection
		private readonly IItemService _itemService;
		private readonly IItemTypesService _itemTypesService;
		public ItemPageController(IItemService itemService, IItemTypesService itemTypesService)
		{
			_itemService = itemService; // Dependancy Injection: Item Service
			_itemTypesService = itemTypesService;
		}



		// List

		public async Task<IActionResult> List()
		{

			IEnumerable<Item> Items = await _itemService.GetItems(); // Get Items from Item Service

			return View(Items);
		}

		// New (Create)

		public IActionResult New()
		{
			return View();
		}

		public async Task<IActionResult> Create(string name, string description, int value)
		{
			await _itemService.CreateItem(name, description, value);
			return RedirectToAction("List");

		}
			

		// Details

		public async Task<IActionResult> Details(int Id)
		{
			Item item = await _itemService.GetItem(Id);
			
			IEnumerable<ItemTypeDto> itemTypes = await _itemTypesService.GetTypesForItem(Id);

			IEnumerable<ItemType> allTypes = await _itemTypesService.GetItemTypes();

			ItemDetailsViewModel itemDetails = new ItemDetailsViewModel
			{
				Item = item,
				ItemTypes = itemTypes,
				AllItemTypes = allTypes
			};

			return View(itemDetails);

			
		}

		// link and unlink

		public async Task<IActionResult> LinkItemToType(int itemId, int typeId)
		{
			await _itemService.LinkItemToType(itemId, typeId);
			return RedirectToAction("Details", new { id = itemId });
		}

		public async Task<IActionResult> UnlinkItemFromType(int itemId, int typeId)
		{
			await _itemService.UnlinkItemToType(itemId, typeId);
			return RedirectToAction("Details", new { id = itemId });
		}

		// Delete

		public async Task<IActionResult> ConfirmDelete(int Id)
		{
			Item item = await _itemService.GetItem(Id);
			return View(item);
			
		}

		public async Task<IActionResult> Delete(int Id)
		{
			await _itemService.DeleteItem(Id);
			return RedirectToAction("List");
		}
		// Edit

		

		public IActionResult Edit()
		{
			return View();
		}

		public async Task<IActionResult> EditItem(int id, string name, string description, int value)
		{
			await _itemService.EditItem(id, name, description, value);
			return RedirectToAction("List");
		}
	}
}
