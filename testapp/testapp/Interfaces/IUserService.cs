﻿using Microsoft.AspNetCore.Mvc;
using testapp.Models;

namespace testapp.Interfaces
{
	public interface IUserService
	{

		// update inventory space

		//Task<IEnumerable<InventoryDto>> ListUserInventory(int userId);

		Task<string> updateUserSpace(int userId, int spaceChange);

		Task<IEnumerable<User>> GetUsers();

		// get individual user
		Task<User> GetUser(int id);

		// create user
		Task<CreateUserDto> PostUser(string username, string password);

		// edit user

		Task<CreateUserDto> EditUser(int id, string username, string password);

		// delete user

		Task<string> DeleteUser(int id);

	}
}
