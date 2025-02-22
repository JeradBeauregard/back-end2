using testapp.Data;
using testapp.Interfaces;
using testapp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace testapp.Services
{
	public class ItemTypesService : IItemTypesService
	{
		//database and interface context

		private readonly ApplicationDbContext _context;
	

		public ItemTypesService(ApplicationDbContext context)
		{
			_context = context;
			
		}

		// list all types
		public async Task<IEnumerable<ItemType>> GetItemTypes()
		{
			return await _context.ItemTypes.ToListAsync();
		}


		// types for an item
		public async Task<IEnumerable<ItemTypeDto>> GetTypesForItem(int itemId)
		{
			List<ItemType> ItemTypes = await _context.ItemTypes
				.Where(it => it.ItemXTypes.Any(ix => ix.ItemId == itemId))
				.ToListAsync();
			List<ItemTypeDto> ItemTypeDtos = new List<ItemTypeDto>();
			foreach (ItemType ItemType in ItemTypes)
			{
				ItemTypeDto itemTypeDto = new ItemTypeDto();
				itemTypeDto.Id = ItemType.Id;
				itemTypeDto.Type = ItemType.Type;
				ItemTypeDtos.Add(itemTypeDto);
			}
			return ItemTypeDtos;
		}

		// create new type

		public async Task<ItemType> CreateItemType(string type)
		{

			ItemType itemType = new ItemType();

			itemType.Type = type;
			_context.ItemTypes.Add(itemType);

			await _context.SaveChangesAsync();

			return itemType;

		}





		}
}
