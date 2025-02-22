using testapp.Data;  
using testapp.Models;  
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using testapp.Interfaces;

namespace testapp.Services  
{
	public class UserService : IUserService
	{
		private readonly ApplicationDbContext _context;

		// Inject ApplicationDbContext
		public UserService(ApplicationDbContext context)
		{
			_context = context;
		}

		// list users
		public async Task<IEnumerable<User>> GetUsers()
		{
			return await _context.Users.ToListAsync();
		}


		/// <summary>
		/// updates inventory space for a user
		/// </summary>
		/// <returns>
		/// An object 
		/*
		 {
           "message": string,
           "userId": int
           "updatedInventorySpace": int
         }
		 */
		/// </returns>
		/// <example>
		/// updateUserSpace(2,2); ->
		/*
	        {
              "message": "Inventory space updated successfully.",
              "userId": 2,
              "updatedInventorySpace": 41
            }
		*/
		/// </example>
		// method for updating user inventory space
		public async Task<string> updateUserSpace(int userId, int spaceChange)
		{
			// find user 
			var user = await _context.Users.FindAsync(userId);
			user.InventorySpace -= spaceChange;
			await _context.SaveChangesAsync();

			return "space updated succesfully";
			// update to return service response

		}

		// create user

		public async Task<CreateUserDto> PostUser(string username, string password)
		{
			User user = new User();

			user.Username = username;
			user.Password = password;
			user.InventorySpace = 50;
			user.SolShards = 5000;

			_context.Users.Add(user);
			await _context.SaveChangesAsync();

			CreateUserDto createUserDto = new CreateUserDto();

			createUserDto.Username = user.Username;
			createUserDto.Password = user.Password;
			createUserDto.InventorySpace = user.InventorySpace;
			createUserDto.SolShards = user.SolShards;
			

			return createUserDto;
		}


	}
}


