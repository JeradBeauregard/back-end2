using Microsoft.AspNetCore.Mvc;
using testapp.Interfaces;
using testapp.Models;

namespace testapp.Controllers
{
	public class InventoryPageController : Controller
	{

		// Dependancy Injection

		private readonly IInventoryService _inventoryService;

		public InventoryPageController(IInventoryService inventoryService)
		{
			_inventoryService = inventoryService; // Dependancy Injection: Inventory Service
		}

		// List

		public async Task<IActionResult> List()
		{

			IEnumerable<InventoryDto> Inventories = await _inventoryService.ListInventories(); // Get Inventorys from Inventory Service
			
			return View(Inventories);
		}

		// New (Create)

		public IActionResult New()
		{
			return View();
		}


		public async Task<IActionResult> Create(int userId, int itemId, int quantity)
		{
			string result = await _inventoryService.AddToInventory(userId, itemId, quantity);
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




		}
}
