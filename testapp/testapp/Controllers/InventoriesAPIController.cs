using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using testapp.Data;
using testapp.Models;
using testapp.Services;

namespace testapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoriesAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserService _userService;

        public InventoriesAPIController(ApplicationDbContext context, UserService userService)
        {
            _context = context;
			_userService = userService;
		}

		/// <summary>
		/// Returns a list of all inventory entries for all users
		/// </summary>
		/// <returns>
		/// [{InvetoryDto},{InventoryDto},..]
		/// </returns>
		/// <example>
		/// GET: api/InvetoriesAPI -> [{InvetoryDto},{InventoryDto},..]
		/// </example>

		// get inventory join with user and item table to view information
		// GET: api/InventoriesAPI
		[HttpGet]
        public async Task<ActionResult<IEnumerable<Inventory>>> ListInventories()
        {
            List<Inventory> Inventories = await _context.Inventory
                .Include(i => i.User)
                .Include(i => i.Item)
                .ToListAsync();

            List<InventoryDto> InventoryDtos = new List<InventoryDto>();
			foreach (Inventory Inventory in Inventories)
			{
			    InventoryDto inventoryDto = new InventoryDto();
                inventoryDto.Id = Inventory.Id;
                inventoryDto.Quantity = Inventory.Quantity;
                inventoryDto.UserId = Inventory.UserId;
                inventoryDto.Username = Inventory.User.Username;
                inventoryDto.ItemId = Inventory.ItemId;
                inventoryDto.ItemName = Inventory.Item.Name;
                InventoryDtos.Add(inventoryDto);
			}

            // add code to order by user id

			return Ok(InventoryDtos);
		}


		/// <summary>
		/// Returns a list of all Inventory entries for a specific user(id)
		/// </summary>
		/// <returns>
		/// [{InvetoryDto},{InventoryDto},..]
		/// </returns>
		/// <example>
		/// GET: api/InvetoriesAPI/2 -> 
          /*[{
            "id": 1,
            "quantity": 1,
            "userId": 2,
            "username": "mango_salad",
            "itemId": 1,
            "itemName": "Blue Rasperberry Desert"
          },
          {
            "id": 3,
            "quantity": 2,
            "userId": 2,
            "username": "mango_salad",
            "itemId": 2,
            "itemName": "Golden Hairbrush"
          },...]
          */
		/// </example>

		// GET: api/InventoriesAPI/Item/5
		[HttpGet("UserInventory/{userId}")]
		public async Task<ActionResult<IEnumerable<Inventory>>> ListUserInventory(int userId)
        {
            

            List<Inventory> Inventories = await _context.Inventory
            .Where(i => i.UserId == userId)
			.Include(i => i.User)
			.Include(i => i.Item)
            .ToListAsync();

			List<InventoryDto> InventoryDtos = new List<InventoryDto>();
			foreach (Inventory Inventory in Inventories)
			{
				InventoryDto inventoryDto = new InventoryDto();
				inventoryDto.Id = Inventory.Id;
				inventoryDto.Quantity = Inventory.Quantity;
				inventoryDto.UserId = Inventory.UserId;
				inventoryDto.Username = Inventory.User.Username;
				inventoryDto.ItemId = Inventory.ItemId;
				inventoryDto.ItemName = Inventory.Item.Name;
				InventoryDtos.Add(inventoryDto);
			}

			return Ok(InventoryDtos);

		}
    


		// GET: api/InventoriesAPI/5
		[HttpGet("{id}")]
        public async Task<ActionResult<Inventory>> GetInventory(int id)
        {
            var inventory = await _context.Inventory.FindAsync(id);

            if (inventory == null)
            {
                return NotFound();
            }

            return inventory;
        }


        // PUT: api/InventoriesAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInventory(int id, Inventory inventory)
        {
            if (id != inventory.Id)
            {
                return BadRequest();
            }

            _context.Entry(inventory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/InventoriesAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Inventory>> PostInventory(Inventory inventory)
        {
            


            _context.Inventory.Add(inventory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInventory", new { id = inventory.Id }, inventory);
		}


		/// <summary>
		/// searches inventory of a user for an item, if item exists quantity is changed to reflect paramters,
        /// if item is not found, item is added to the users inventory based on paramters
        /// UserService is called to adjust inventory space based on changed/added item quantity
		/// </summary>
		/// <param name="InventoryDto">The required information to add the inventory item (userId,itemId,quantity)</param>
		/// <example>
		/// POST api/InventoryAPI/AddToInventory/2/2/2
		/// </example>
		/// <returns>
		///
             /*{
                    "id": 3,
              "quantity": 4,
              "userId": 2,
              "itemId": 2,
              "user": {
                "id": 2,
                "username": "mango_salad",
                "password": "mango123",
                "inventorySpace": 43,
                "solShards": 5000
              },
              "item": null
            } */
		/// </returns>
		// this is my inventory add, simpler inputs than a fulljson like the template
		// POST: api/InventoriesAPI/AddToInventory
		[HttpPost("AddToInventory/{userId}/{quantity}/{itemId}")]
		public async Task<ActionResult<Inventory>> AddToInventory(int userId, int itemId, int quantity)
		{
	

			//Check if the item already exists in the user's inventory
			var inventoryItem = await _context.Inventory
				.FirstOrDefaultAsync(i => i.UserId == userId && i.ItemId == itemId);

			if (inventoryItem != null)
			{
				// If item exists, increase its quantity
				inventoryItem.Quantity += quantity;
              

				await _context.SaveChangesAsync();
				var userSpaceReturn = await _userService.updateUserSpace(userId, quantity);
                Console.WriteLine(userSpaceReturn);


				// Return the created inventory entry
				return CreatedAtAction("GetInventory", new { id = inventoryItem.Id }, inventoryItem);
			}

            else
            {

			// Create new inventory entry
			var NewItem = new Inventory
			{
				UserId = userId,
				ItemId = itemId,
				Quantity = quantity
			};

			// Add to the database
			_context.Inventory.Add(NewItem);
			await _context.SaveChangesAsync();
			var userSpaceReturn = await _userService.updateUserSpace(userId, quantity);
			Console.WriteLine(userSpaceReturn);

				// Return the created inventory entry
				return CreatedAtAction("GetInventory", new { id = NewItem.Id }, NewItem);

                }
		}


		// DELETE: api/InventoriesAPI/5
		[HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventory(int id)
        {
            var inventory = await _context.Inventory.FindAsync(id);
            if (inventory == null)
            {
                return NotFound();
            }

            _context.Inventory.Remove(inventory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InventoryExists(int id)
        {
            return _context.Inventory.Any(e => e.Id == id);
        }
    }
}
