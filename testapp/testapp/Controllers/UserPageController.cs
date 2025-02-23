﻿using Microsoft.AspNetCore.Mvc;
using testapp.Interfaces;
using testapp.Models;

namespace testapp.Controllers
{
	public class UserPageController : Controller
	{

		// Dependancy Injection	
		private readonly IUserService _userService;

		public UserPageController(IUserService userService)
		{
			_userService = userService; // Dependancy Injection: User Service
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
			return View(user);
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

		public async Task<IActionResult> EditUser(int id, string username, string password)
		{
			await _userService.EditUser(id, username, password);
			return RedirectToAction("List");
		}
	}
}
