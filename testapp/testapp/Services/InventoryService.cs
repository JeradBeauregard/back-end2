using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using testapp.Data;
using testapp.Interfaces;
using testapp.Models;

namespace testapp.Services
{
	public class InventoryService : IInventoryService
	{
		private readonly ApplicationDbContext _context;
		private readonly IUserService _userService;

		// Inject ApplicationDbContext
		public InventoryService(ApplicationDbContext context, IUserService userService)
		{
			_context = context;
			_userService = userService;
		}

		// list inventory items for a user

		public async Task<IEnumerable<InventoryDto>> ListUserInventory(int userId)
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

			return InventoryDtos;

		}

		// list all inventory entries with item information
		public async Task<IEnumerable<InventoryDto>> ListInventories()
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

			return InventoryDtos;
		}


		// add item and update user space(calling user service)

		public async Task<string> AddToInventory(int userId, int itemId, int quantity)
		{


			//Check if the item already exists in the user's inventory
			var inventoryItem = await _context.Inventory
				.FirstOrDefaultAsync(i => i.UserId == userId && i.ItemId == itemId);

			if (inventoryItem != null)
			{
				// If item exists, increase its quantity
				inventoryItem.Quantity += quantity;


				await _context.SaveChangesAsync();
				string userSpaceReturn = await _userService.updateUserSpace(userId, quantity);
				Console.WriteLine(userSpaceReturn);


				// Return the created inventory entry
				return "success, queantity increased";
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
				string userSpaceReturn = await _userService.updateUserSpace(userId, quantity);
				Console.WriteLine(userSpaceReturn);

				// Return the created inventory entry
				return "success... inventory item added";

				// update to return a service response

			}
		}

			// delete inventory entry


			// delete an inventory entry by id
			// DELETE: api/InventoriesAPI/5
			
			public async Task<string> DeleteInventory(int id)
			{
				var inventory = await _context.Inventory.FindAsync(id);
				if (inventory == null)
				{
					return "inventory entry not found";
				}

				_context.Inventory.Remove(inventory);
				await _context.SaveChangesAsync();

				return "entry deleted";
			}
		}


	}

