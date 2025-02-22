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
            var itemType = await _context.ItemTypes.FindAsync(id);

            if (itemType == null)
            {
                return NotFound();
            }

            return itemType;
        }

        // PUT: api/ItemTypesAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemType(int id, ItemType itemType)
        {
            if (id != itemType.Id)
            {
                return BadRequest();
            }

            _context.Entry(itemType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemTypeExists(id))
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

        // POST: api/ItemTypesAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ItemType>> PostItemType(ItemType itemType)
        {
            _context.ItemTypes.Add(itemType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItemType", new { id = itemType.Id }, itemType);
        }

        // DELETE: api/ItemTypesAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemType(int id)
        {
            var itemType = await _context.ItemTypes.FindAsync(id);
            if (itemType == null)
            {
                return NotFound();
            }

            _context.ItemTypes.Remove(itemType);
            await _context.SaveChangesAsync();

            return NoContent();
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
    }
}