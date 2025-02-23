using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using testapp.Data;
using testapp.Models;
using testapp.Services;

namespace testapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersAPIController : ControllerBase
    {
		private readonly ApplicationDbContext _context;
		private readonly UserService _userService;  

		// Inject both ApplicationDbContext and UserService
		public UsersAPIController(ApplicationDbContext context, UserService userService)
		{
			_context = context;
			_userService = userService; 
		}
		/// <summary>
		/// updates inventory space using a user service
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
		/// GET: api/UserAPI/updateInventorySpace/2/2 -> 
		/*
	        {
              "message": "Inventory space updated successfully.",
              "userId": 2,
              "updatedInventorySpace": 41
            }
		*/
		/// </example>

		// PUT api/UsersAPI/updateInventorySpace
		[HttpPut("updateInventorySpace/{userId}/{spaceRemoved}")]

        public async Task<ActionResult<Object>> updateUserSpace(int userId, int spaceRemoved)
        {

			return await _userService.updateUserSpace(userId, spaceRemoved);

		}



        // GET: api/UsersAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/UsersAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        

        // POST: api/UsersAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CreateUserDto>> PostUser(string username, string password)
        {

			CreateUserDto result = await _userService.PostUser(username, password);

            return result;
        }

		//POST: api/UsersAPI/Edit

		[HttpPost("Edit")]
		public async Task<ActionResult<CreateUserDto>> EditUser(int id, string username, string password)
		{
			CreateUserDto result = await _userService.EditUser(id, username, password);
			return result;
		}

		// DELETE: api/UsersAPI/5
		[HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteUser(int id)
        {
            string Result = await _userService.DeleteUser(id);
			return Result;
		}

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
