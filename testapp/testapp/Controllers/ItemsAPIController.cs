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
		// get item names with item types
		// GET: api/ItemsAPI
		[HttpGet]
        public async Task<IEnumerable<Item>> GetItems()
        {
            return await _itemService.GetItems();
        }

        [HttpGet("WithTypes")]

        public async Task<IEnumerable<ItemWithTypesDto>> GetItemsWithTypes()
		{
            IEnumerable<ItemWithTypesDto> results = await _itemService.GetItemsWithTypes();

            return results;
        }

        // GET: api/ItemsAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItem(int id)
        {
            Item item = await _itemService.GetItem(id);
            return item;
        }

   
        
        // DELETE: api/ItemsAPI/5
        [HttpDelete("{id}")]
        public async Task<string> DeleteItem(int id)
        {
           string result = await _itemService.DeleteItem(id);
            return result;
        }

    
    }
}
