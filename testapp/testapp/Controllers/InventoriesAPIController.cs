﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using testapp.Data;
using testapp.Models;
using testapp.Services;
using testapp.Interfaces;

namespace testapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoriesAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserService _userService;
        private readonly IInventoryService _inventoryService;


        public InventoriesAPIController(ApplicationDbContext context, UserService userService, IInventoryService inventoryService)
        {
            _context = context;
            _userService = userService;
            _inventoryService = inventoryService;
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

        // read
        // get inventory join with user and item table to view information
        // GET: api/InventoriesAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Inventory>>> ListInventories()
        {
            // new empty list of Dtos
            IEnumerable<InventoryDto> InventoryDtos = await _inventoryService.ListInventories();

            // return 200 OK with Dtos

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
        //read
        // GET: api/InventoriesAPI/Item/5
        [HttpGet("UserInventory/{userId}")]
        public async Task<ActionResult<IEnumerable<InventoryDto>>> ListUserInventory(int userId)
        {
            // new empty list of Dtos
            IEnumerable<InventoryDto> InventoryDtos = await _inventoryService.ListUserInventory(userId);

            // return 200 OK with Dtos

            return Ok(InventoryDtos);

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
        public async Task<ActionResult<string>> AddToInventory(int userId, int itemId, int quantity)
        {

            string result = await _inventoryService.AddToInventory(userId, itemId, quantity);

            return result;

        }

        // edit inventory
        [HttpPost("EditInventory/{id}/{userId}/{itemId}/{quantity}")]
		public async Task<ActionResult<InventoryDto>> EditInventory(int id, int userId, int itemId, int quantity)
        {
            InventoryDto Result = await _inventoryService.EditInventory(id,userId,itemId,quantity);
            return Ok(Result);

		}

        // delete an inventory entry by id
        // DELETE: api/InventoriesAPI/5
        [HttpDelete("{id}")]
        public async Task<string> DeleteInventory(int id)
        {
            
            string result = await _inventoryService.DeleteInventory(id);

            return result;
        }


    }
}
