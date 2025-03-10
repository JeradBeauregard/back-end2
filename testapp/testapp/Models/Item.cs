﻿using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace testapp.Models
{
	public class Item
	{
		[Key]
		public int Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }	

		public int Value { get; set; }

		public Boolean HasPic { get; set; } = false;

		public string? PicPath { get; set; }


		public virtual List<ItemxType> ItemxType { get; set; }

	}

	public class ItemDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int Value { get; set; }

		public string? PicPath { get; set; }
		public virtual List<ItemxType> ItemxType { get; set; }
	}

	public class ItemsForTypeDto
	{
		public int Id { get; set; }
		public string Name { get; set; }	

	}

	public class ItemWithTypesDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public List<string> Types { get; set; }
	}

	public class CreateItemDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int Value { get; set; }

	}

	public class UserByItemDto
	{
		public int Id { get; set; }

		public string ItemName { get; set; }

		public int Quantity { get; set; }

		public string Username { get; set; }

		public int UserId { get; set; }


	}
	// model for item view model (contains item and item types information)
	public class ItemDetailsViewModel
	{
		public ItemDto Item { get; set; }
		public IEnumerable<ItemTypeDto> ItemTypes { get; set; }

		public IEnumerable<ItemType> AllItemTypes { get; set; }

		public IEnumerable<UserByItemDto> UserByItem { get; set; }

		public int TotalAmount { get; set; }
	}

}
