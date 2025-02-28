using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Model.Strings;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using testapp.Data;
using testapp.Interfaces;
using testapp.Models;

namespace testapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IItemService _itemService;

        public ItemsAPIController(ApplicationDbContext context, IItemService itemService)
        {
            _context = context;
            _itemService = itemService;
        }

	
		// get item names with item types
		// GET: api/ItemsAPI
		[HttpGet]
        public async Task<IEnumerable<Item>> GetItems()
        {
            return await _itemService.GetItems();
        }

		/// <summary>
		/// Returns a list of all items with their types
		/// </summary>
		/// <returns>
		/// [{ItemDto},{ItemDto},..]
		/// </returns>
		/// <example>
		/// GET: api/ItemsAPI -> 
		/*
		         [
          {
            "id": 1,
            "name": "Blue Rasperberry Desert",
            "types": [
              "Food",
              "Desert"
            ]
          },
          {
            "id": 2,
            "name": "Golden Hairbrush",
            "types": [
              "Beauty",
              "Luxury"
            ]
          },
          {
            "id": 3,
            "name": "Turkey Leg",
            "types": [
              "Food"
            ]
          }
        ]
		*/
		/// </example>
		[HttpGet("WithTypes")]

        public async Task<IEnumerable<ItemWithTypesDto>> GetItemsWithTypes()
		{
            IEnumerable<ItemWithTypesDto> results = await _itemService.GetItemsWithTypes();

            return results;
        }

        // returns all items of a given type

        [HttpGet("ItemsAPI/GetItemsForType")]

		public async Task<IEnumerable<ItemsForTypeDto>> GetItemsForType(int typeId)
        {
			IEnumerable<ItemsForTypeDto> results = await _itemService.GetItemsForType(typeId);
			return results;
		}

		// GET: api/ItemsAPI/5
		[HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetItem(int id)
        {
            ItemDto item = await _itemService.GetItem(id);
            return item;
        }

   
        
        // DELETE: api/ItemsAPI/5
        [HttpDelete("{id}")]
        public async Task<string> DeleteItem(int id)
        {
           string result = await _itemService.DeleteItem(id);
            return result;
        }

        [HttpPost("ItemsAPI/LinkItemToType")]
        public async Task<string> LinkItemToType(int itemId, int typeId)
		{
			string result = await _itemService.LinkItemToType(itemId, typeId);
			return result;
		}

        [HttpPost("ItemsAPI/UnlinkItemToType")]
		public async Task<string> UnlinkItemToType(int itemId, int typeId)
		{
			string result = await _itemService.UnlinkItemToType(itemId, typeId);
			return result;
		}

        [HttpPost("ItemsAPI/CreateItem")]

		public async Task<CreateItemDto> CreateItem(string name, string description, int value)
        {

            CreateItemDto result = await _itemService.CreateItem(name, description, value);
            
			return result;
		}

		[HttpPost("ItemsAPI/EditItem")]

		public async Task<CreateItemDto> EditItem(int id, string name, string description, int value)
		{
			CreateItemDto result = await _itemService.EditItem(id, name, description, value);
			return result;
		}


	}
}
