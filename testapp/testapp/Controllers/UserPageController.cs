using Microsoft.AspNetCore.Mvc;
using testapp.Interfaces;
using testapp.Models;

namespace testapp.Controllers
{
	public class UserPageController : Controller
	{

		// Dependancy Injection	
		private readonly IUserService _userService;
		private readonly IInventoryService _inventoryService;

		public UserPageController(IUserService userService, IInventoryService inventoryService)
		{
			_userService = userService; // Dependancy Injection: User Service
			_inventoryService = inventoryService;	
		}

		// List

		public async Task<IActionResult> List()
		{

			IEnumerable<User> Users = await _userService.GetUsers(); // Get Users from User Service
			return View(Users);
		}

		// New (Create)

		public IActionResult New()
		{
			return View();
		}

		public async Task<IActionResult> Create(string username, string password)
		{
			await _userService.PostUser(username, password);
			return RedirectToAction("List");
		}

		// Details

		public async  Task<IActionResult> Details(int id)
		{
			User user = await _userService.GetUser(id);
			IEnumerable<InventoryDto> inventory = await _inventoryService.ListUserInventory(id);

			UserDetailsViewModel userDetails = new UserDetailsViewModel
			{
				User = user,
				Inventory = inventory
			};

			return View(userDetails);
		}

		// Delete

		public async Task<IActionResult> ConfirmDelete(int Id)
		{
			
			User user = await _userService.GetUser(Id);

			return View(user);
		}

		public async Task<IActionResult> Delete(int Id)
		{
			await _userService.DeleteUser(Id);
			return RedirectToAction("List");
		}

		// Edit

		public IActionResult Edit()
		{
			return View();
		}

		public async Task<IActionResult> EditUser(int id, string username, string password)
		{
			await _userService.EditUser(id, username, password);
			return RedirectToAction("List");
		}
	}
}
