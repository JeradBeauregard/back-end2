using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using testapp.Data;
using testapp.Interfaces;
using testapp.Models;

namespace testapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemTypesAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IItemTypesService _itemTypesService;

        public ItemTypesAPIController(ApplicationDbContext context, IItemTypesService itemTypesService)
        {
            _context = context;
            _itemTypesService = itemTypesService;
        }

        // GET: api/ItemTypesAPI

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemType>>> GetItemTypes()
        {
            return await _context.ItemTypes.ToListAsync();
        }

        // GET: api/ItemTypesAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemType>> GetItemType(int id)
        {
            ItemType itemType = await _itemTypesService.GetItemType(id);
            return itemType;
        }





		private bool ItemTypeExists(int id)
        {
            return _context.ItemTypes.Any(e => e.Id == id);
        }



        // get types for an item
        [HttpGet("ItemTypesAPI/GetTypesForItem")]

        public async Task<IEnumerable<ItemTypeDto>> GetTypesForItem(int itemId)
        {
            IEnumerable<ItemTypeDto> Result = await _itemTypesService.GetTypesForItem(itemId);
            return Result;
        }

        // create new type

        [HttpPost("ItemTypesAPI/CreateItemType")]

        public async Task<ItemType> CreateItemType(string type)
        {
            ItemType Result = await _itemTypesService.CreateItemType(type);
            return Result;


        }
        // delete Item type

        [HttpDelete("ItemTypesAPI/DeleteItemType/{id}")]

		public async Task<string> DeleteItemType(int id)
		{
			string Result = await _itemTypesService.DeleteItemType(id);
			return Result;
		}
	}
}