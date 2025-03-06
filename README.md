# back-end2
Repository for back-end2 course material

## Methods

Inventory Service
Read:

Task<IEnumerable<InventoryDto>> ListUserInventory(int userId);
Task<IEnumerable<InventoryDto>> ListInventories();
Task<InventoryDto> ListInventory(int id);
Task<int> TotalAmountofItem(int itemId);

Create:

Task<string> AddToInventory(int userId, int itemId, int quantity);

Update:

Task<InventoryDto>EditInventory(int id, int userId, int itemId, int quantity);
Task<string> UpdateQuantity(int id, int quantity);

Delete:

Task<string> DeleteInventory(int id);

Item Service
Read:

Task<IEnumerable<UserByItemDto>> ListUsersByItem(int itemId);
Task<IEnumerable<Item>> GetItems();
Task<ItemDto> GetItem(int id);
Task<IEnumerable<ItemWithTypesDto>> GetItemsWithTypes();

Create:

Task<CreateItemDto> CreateItem(string name, string description, int value);

Update:

Task<string> LinkItemToType(int itemId, int typeId);
Task<string> UnlinkItemToType(int itemId, int typeId);
Task<CreateItemDto> EditItem(int id, string name, string description, int value);
Task<string>AddItemImage(int id,IFormFile image);

Delete:

Task<string> DeleteItem(int id);

User Service
Read:

Task<IEnumerable<User>> GetUsers();
Task<User> GetUser(int id);

Create:

Task<CreateUserDto> PostUser(string username, string password);

Update:

Task<CreateUserDto> EditUser(int id, string username, string password, int solshards);
Task<string> updateUserSpace(int userId, int spaceChange);

Delete:

Task<string> DeleteUser(int id);

ItemTypes Service
Read:

Task<IEnumerable<ItemType>> GetItemTypes();
Task<IEnumerable<ItemTypeDto>> GetTypesForItem(int itemId);
Task<ItemType> GetItemType(int id);


Create:

Task<ItemType> CreateItemType(string type);

Update:

Task<ItemType> EditItemType(int id, string type);

Delete:

Task<string> DeleteItemType(int id);

API Actions

Inventories:

user can list all inventories at - // GET: api/InventoriesAPI
user can list a specific inventory by user id at - GET: api/InventoriesAPI/UserInventory/{userId}
user can add an item to a user's inventory at - POST: api/InventoriesAPI/AddToInventory/{userId}/{quantity}/{itemId}
user can edit and inventory entry at - POST: /api/InventoriesAPI/EditInventory/{id}/{userId}/{itemId}/{quantity}
user can delete an inventory entry at - DELETE: /api/InventoriesAPI/{id}

Items:

user can list all items (without types) at - GET: /api/ItemsAPI
user can list all items with their types at - GET: api/ItemsAPI/WithTypes
user can get a list of all the items of a given type at - GET: /api/ItemsAPI/ItemsAPI/GetItemsForType
user can get an item by its id at - GET: /api/ItemsAPI/{id}
user can delete and item by its id at - DELETE: /api/ItemsAPI/{id}
user can link a type to and item at - POST: /api/ItemsAPI/ItemsAPI/LinkItemToType/{itemId}/{typeId}
user can unlink a type from and item at - POST: /api/ItemsAPI/ItemsAPI/UnlinkItemToType/{itemId}/{typeId}
user can create a new item at - POST: /api/ItemsAPI/ItemsAPI/CreateItem/{name}/{description}/{value}
user can edit an existing item at - POST: /api/ItemsAPI/ItemsAPI/EditItem

ItemTypes:

user can get a list of all item types at - GET: /api/ItemTypesAPI
user can get a single item type by id at - GET: /api/ItemTypesAPI/{id}
user can get a list of types linked to a given item at - GET: /api/ItemTypesAPI/ItemTypesAPI/GetTypesForItem
user can create a new item type at - POST: /api/ItemTypesAPI/ItemTypesAPI/CreateItemType/{type}
user can delete an item type by id at - DELETE: /api/ItemTypesAPI/ItemTypesAPI/DeleteItemType/{id}

Users:

user can update a user's inventory space at - PUT: api/UsersAPI/updateInventorySpace/{userId}/{spaceRemoved}
user can get a list of all users at - GET: /api/UsersAPI
user can get a single user by id at - GET: /api/UsersAPI/{id}
user can delete a user by id at - DELETE: /api/UsersAPI/{id}
user can create a new user at - POST: /api/UsersAPI/PostUser/{username}/{password}
user can edit an existing user at - POST: /api/UsersAPI/EditUser/{id}/{username}/{password}/{solshards}

Pages

/ ->
Home page containts links to manage
- Users (UserPage/List)
- Inventory (InventoryPage/List)
- Items (ItemPage/List)
- Item Types (ItemTypePage/List)

/UserPage/List
- Lists all users, each user links to user details (/UserPage/Details/{id})
- Links to create new user page (UserPage/New)

/UserPage/New
- Allows user to submit a form to create a new user
- returns to UserPage/List upon successful submission

UserPage/Details/{id}
- lists user information (username, password, sol-shards, inventory space)
- Links to Edit User (UserPage/Edit/{id}
- links to Delete user Page
- Allows user to add an item to that user's inventory directly
- lists inventory for this user
- allows inventory entry's to be edited directly for that user
- Inventory entries also link to their items detail pages

/UserPage/Edit/{id}
- Form that allows the given user to be edited
- returns to users details upon succesful submission

/UserPage/ConfirmDelete/{id}
- confirm delete message
- yes, deletes user and returns to user list
- no, cancels deletion and returns to user details

/InventoryPage/List
- Lists all inventory entries
- Allows individual entries to have their quantity updated
- Allows individual entries to be deleted
- link to create a new inventory entry (/InventoryPage/New)

/InventoryPage/New
- For that allows a new entry to be added to the inventory table
- selections are locked to existing data (users,items)
- returns to inventory list upon successful submission

/ItemPage/List
- lists all items
- each item listed links to item details (/ItemPage/Details/{id})
- links to create a new item (ItemPage/New)

/ItemPage/New
- Form that allows a new item to be added
- Returns to item list upon successful submission

/ItemPage/Details/{id}
- shows uploaded image if available
- lists item information (id, description, value, amount in existance)
- lists types associated with the item, links to type details
- allows existing types to be removed
- allows new types to be linked
- link to edit the item (/ItemPage/Edit/{id})
- link to delete item (/ItemPage/ConfirmDelete/{id})
- displays all users that own the item, and the amount each user owns
- users displayed link to user details

/ItemPage/Edit/{id}
- Form that allows user to edit the item information
- returns to item details upon successful submission

/ItemPage/ConfirmDelete/{id}
- confirm delete message
- yes deletes item and return to item list
- no cancels deletion and returns to item details

/ItemTypePage/List
- lists all item types
- Each listed item type links to that item type's details (/ItemTypePage/Details/{id})
- links to create new item type page (/ItemTypePage/New)

/ItemTypePage/New
- Form that allows the user to create a new item type
- returns to item type list upon successful completion

/ItemTypePage/Details/{id}
- shows item type name
- shows list of items with this item type, items link to item details
- links to item type delete (/ItemTypePage/ConfirmDelete/{id})

/ItemTypePage/ConfirmDelete/{id}
- Confirm delete message
- yes deletes item type and returns to item type page
- no cancels deletion and returns to item type details







