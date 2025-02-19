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


	}
}
