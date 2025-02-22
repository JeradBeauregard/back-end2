using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using testapp.Interfaces;
using testapp.Models;

namespace testapp.Controllers
{
	public class ItemPageController : Controller
	{
		// Dependancy Injection
		private readonly IItemService _itemService;

		public ItemPageController(IItemService itemService)
		{
			_itemService = itemService; // Dependancy Injection: Item Service
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

		public IActionResult Details()
		{
			return View();
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

		public async Task<IActionResult> EditItem(int id, string name, string description, int value)
		{
			await _itemService.EditItem(id, name, description, value);
			return RedirectToAction("List");
		}
	}
}
