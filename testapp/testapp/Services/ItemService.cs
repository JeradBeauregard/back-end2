using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using testapp.Data;
using testapp.Interfaces;
using testapp.Models;

namespace testapp.Services
{
	public class ItemService : IItemService
	{

		// database and interface context (dependacy injection is so hot)

		private readonly ApplicationDbContext _context;

		public ItemService(ApplicationDbContext context)
		{
			_context = context;
		}

		// read

		// list users that own a specific item

		public async Task<IEnumerable<UserByItemDto>> ListUsersByItem(int itemId)
		{
			List<Inventory> inventories = await _context.Inventory
				.Where(i => i.ItemId == itemId)
				.Include(i => i.User)
				.Include(i => i.Item)
				.ToListAsync();

			IEnumerable<UserByItemDto> userByItemDtos = inventories.Select(i => new UserByItemDto
			{
				UserId = i.UserId,
				Username = i.User.Username,
				Quantity = i.Quantity
			});


			return userByItemDtos;
		}

		// get single item
		public async Task<Item> GetItem(int id)
		{
			var item = await _context.Items.FindAsync(id);

			if (item == null)
			{

				Item itemError = new Item();

				itemError.Name = "Item not found, this is an error message. NOT a real item";
				return itemError;
			}

			return item;
		}

		// get all items (without types)

		public async Task<IEnumerable<Item>> GetItems()
		{
			return await _context.Items.ToListAsync();
		}

		// get all items with types
		public async Task<IEnumerable<ItemWithTypesDto>> GetItemsWithTypes()
		{
			List<Item> Items = await _context.Items
				.Include(i => i.ItemxType)
					.ThenInclude(ix => ix.ItemType)
				.ToListAsync();

			List<ItemWithTypesDto> ItemDtos = new List<ItemWithTypesDto>();
			foreach (Item Item in Items)
			{
				ItemWithTypesDto itemWithTypesDto = new ItemWithTypesDto();
				itemWithTypesDto.Id = Item.Id;
				itemWithTypesDto.Name = Item.Name;
				itemWithTypesDto.Types = new List<string>();

				foreach (ItemxType ix in Item.ItemxType)
				{

					itemWithTypesDto.Types.Add(ix.ItemType.Type);
				}



				ItemDtos.Add(itemWithTypesDto);
			}


			return ItemDtos;

		}

		// get all items per a type
		public async Task<IEnumerable<ItemsForTypeDto>> GetItemsForType(int typeId)
		{
			ItemType itemType = await _context.ItemTypes.FindAsync(typeId);
			if (itemType == null)
			{
				return null;
			}
			List<Item> Items = await _context.Items
				.Include(i => i.ItemxType)
					.ThenInclude(ix => ix.ItemType)
				.Where(i => i.ItemxType.Any(ix => ix.ItemType == itemType))
				.ToListAsync();

			List<ItemsForTypeDto> ItemsForType = new List<ItemsForTypeDto>();
			foreach (Item Item in Items)
			{
				ItemsForTypeDto itemForType = new ItemsForTypeDto();
				itemForType.Id = Item.Id;
				itemForType.Name = Item.Name;
				ItemsForType.Add(itemForType);
			}

			return ItemsForType;
		}
		// delete item

		public async Task<string> DeleteItem(int id)
		{
			var item = await _context.Items.FindAsync(id);
			if (item == null)
			{
				return "item to be deleted not found";
			}

			_context.Items.Remove(item);
			await _context.SaveChangesAsync();

			return "item succesfully deleted.";
		}

		//link item to item type
		public async Task<string> LinkItemToType(int itemId, int typeId)
		{
			Item item = await _context.Items.FindAsync(itemId);
			ItemType itemType = await _context.ItemTypes.FindAsync(typeId);
			if (itemType == null && item == null)
			{
				return "item and item type not found";
			}
			if (item == null)
			{
				return "item not found";
			}
			if (itemType == null)
			{
				return "item type not found";
			}
			ItemxType itemxType = new ItemxType();
			itemxType.Item = item;
			itemxType.ItemType = itemType;
			_context.ItemxTypes.Add(itemxType);
			await _context.SaveChangesAsync();
			return "item linked to type";

		}

		public async Task<string> UnlinkItemToType(int itemId, int typeId)
		{
			Item item = await _context.Items.FindAsync(itemId);
			ItemType itemType = await _context.ItemTypes.FindAsync(typeId);
			if (itemType == null && item == null)
			{
				return "item and item type not found";
			}
			if (item == null)
			{
				return "item not found";
			}
			if (itemType == null)
			{
				return "item type not found";
			}
			ItemxType itemxType = await _context.ItemxTypes
				.Where(ix => ix.Item == item)
				.Where(ix => ix.ItemType == itemType)
				.FirstOrDefaultAsync();
			if (itemxType == null)
			{
				return "item not linked to type";
			}
			_context.ItemxTypes.Remove(itemxType);
			await _context.SaveChangesAsync();
			return "item unlinked from type";
		}

		// create item

		public async Task<CreateItemDto> CreateItem(string name, string description, int value)
		{

			Item item = new Item();

			item.Name = name;
			item.Description = description;
			item.Value = value;

			_context.Items.Add(item);
			await _context.SaveChangesAsync();

			CreateItemDto createItemDto = new CreateItemDto();

			createItemDto.Id = item.Id;
			createItemDto.Name = item.Name;
			createItemDto.Description = item.Description;
			createItemDto.Value = item.Value;



			return createItemDto;
		}

		// edit existing item

		public async Task<CreateItemDto> EditItem(int id, string name, string description, int value)
		{
			Item item = await _context.Items.FindAsync(id);

			
			item.Name = name;
			item.Description = description;
			item.Value = value;
			_context.Items.Update(item);
			await _context.SaveChangesAsync();

			CreateItemDto createItemDto = new CreateItemDto();
			createItemDto.Id = item.Id;
			createItemDto.Name = item.Name;
			createItemDto.Description = item.Description;
			createItemDto.Value = item.Value;
			return createItemDto;
		}
	}
}
