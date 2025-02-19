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


	}
}


