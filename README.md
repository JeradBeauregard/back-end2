# back-end2
Repository for back-end2 course material

## Methods created/edited by me

Inventory controller:

ListInventories() 
ListUserInventory(int userId)
AddToInventory(int userId, int itemId, int quantity)

Item controller:

GetItemWithTypes()

User Controller:

updateUserSpace(int userId, int spaceRemoved)

UserService:

updateUserSpace(int userId, int spaceChange)

user can list all inventories at - // GET: api/InventoriesAPI
user can list a specific inventory by user id at - GET: api/InventoriesAPI/UserInventory/{userId}
user can add an item to a user's inventory at - POST: api/InventoriesAPI/AddToInventory/{userId}/{quantity}/{itemId}
user can list all items with their types at - GET: api/ItemsAPI/WithTypes
user can update a user's inventory space at - PUT api/UsersAPI/updateInventorySpace/{userId}/{spaceRemoved}
