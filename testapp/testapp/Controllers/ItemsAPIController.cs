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
using testapp.Models;

namespace testapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ItemsAPIController(ApplicationDbContext context)
        {
            _context = context;
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
        public async Task<ActionResult<IEnumerable<Item>>> GetItems()
        {
            return await _context.Items.ToListAsync();
        }

        [HttpGet("WithTypes")]

        public async Task<ActionResult<IEnumerable<Item>>> GetItemWithTypes()
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

           
            return Ok(ItemDtos);

        }

        // GET: api/ItemsAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItem(int id)
        {
            var item = await _context.Items.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        // PUT: api/ItemsAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItem(int id, Item item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
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

        // POST: api/ItemsAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Item>> PostItem(Item item)
        {
            _context.Items.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItem", new { id = item.Id }, item);
        }

        // DELETE: api/ItemsAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.Id == id);
        }
    }
}
