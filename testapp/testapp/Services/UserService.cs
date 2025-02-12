using testapp.Data;  
using testapp.Models;  
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace testapp.Services  
{
	public class UserService
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
		public async Task<ActionResult<Object>> updateUserSpace(int userId, int spaceChange)
		{
			// find user 
			var user = await _context.Users.FindAsync(userId);
			user.InventorySpace -= spaceChange;
			await _context.SaveChangesAsync();

			return new OkObjectResult(new
			{
				message = "Inventory space updated successfully.",
				userId = user.Id,
				updatedInventorySpace = user.InventorySpace
			});

		}


	}
}


