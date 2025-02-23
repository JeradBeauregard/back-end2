﻿using Microsoft.AspNetCore.Mvc;
using testapp.Interfaces;
using testapp.Models;

namespace testapp.Controllers
{
	public class ItemTypePageController : Controller
	{

		// Dependancy Injection

		private readonly IItemTypesService _itemTypeService;

		public ItemTypePageController(IItemTypesService itemTypeService)
		{
			_itemTypeService = itemTypeService; // Dependancy Injection: ItemType Service
		}


		// List

		public async Task<IActionResult> List()
		{

			IEnumerable<ItemType> ItemTypes = await _itemTypeService.GetItemTypes(); // Get ItemTypes from ItemType Service

			return View(ItemTypes);
		}

		// New (Create)

		public IActionResult New()
		{
			return View();
		}

		public async Task<IActionResult> Create(string type)
		{
			await _itemTypeService.CreateItemType(type);
			return RedirectToAction("List");
		}

		// Details

		public async Task<IActionResult> Details(int Id)
		{

			ItemType itemType = await _itemTypeService.GetItemType(Id);
			return View(itemType);
		}

		// Delete

		public IActionResult confirmDelete()
		{
			return View();
		}

		// Edit

		public IActionResult Edit()
		{
			return View();
		}

		public async Task<IActionResult> EditItemType(int id, string type)
		{
			await _itemTypeService.EditItemType(id, type);
			return RedirectToAction("List");
		}
	}
}
