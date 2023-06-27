using GroceryCartAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace GroceryCartAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeworkController : Controller
    {
        private static List<GroceryItem>? groceryItems;

        public HomeworkController()
        {
            InitializeList();
        }

        private void InitializeList()
        {
            if (groceryItems == null)
            {
                groceryItems = new List<GroceryItem>();
                
                List<string> items = new List<string>()
                {
                    "Apple", "Banana", "Carrot", "Lemon", "Strawberry", "Chicken",
                    "Grapes", "Watermelon", "Pineapple", "Kiwi", "Peach", "Pork",
                    "Lettuce", "Tomato", "Onion", "Garlic", "Pepper", "Salt",
                    "Sugar", "Eggplant", "Spinach", "Beans", "Beef",  "Tuna",
                    "Crab", "Shrimp", "Lobster", "Coffee", "Milk", "Chocolate" 
                };

                Random random = new Random();

                for (int i = 1; i <= 30; i++)
                {
                   
                    var item = new GroceryItem
                    {
                        ItemId = i,
                        ItemName = items[i - 1],
                        Price = random.Next(10, 100),
                        Quantity = random.Next(1,10)
                    };

                    groceryItems.Add(item);
                }
            }
        }

        [HttpGet("GetProductByID", Name = "GetProductByID")]
        public IActionResult GetByID(int id)
        {
            if (groceryItems == null)
            {
                return NotFound("Product not found!");
            }

            var existingItem = groceryItems.FirstOrDefault(x => x.ItemId == id);

            if (existingItem != null)
            {
                return Ok(existingItem);
            }
            else
            {
                return NotFound("Product not found!");
            }
        }
        [HttpPost("AddProducts", Name = "AddProducts")]
        public IActionResult Post(List<GroceryItem> newItems)
        {
            if (groceryItems == null)
            {
                groceryItems = new List<GroceryItem>();
            }

            foreach (var newItem in newItems)
            {
                if (groceryItems.Any(x => x?.ItemId == newItem?.ItemId))
                {
                    return BadRequest("Item already exists!");
                }
            }

            groceryItems.AddRange(newItems);
            return Ok("Items added successfully.");
        }


        [HttpPut("UpdateProduct", Name = "UpdateProduct")]
        public IActionResult Put(GroceryItem groceryItem)
        {
            if (groceryItems == null)
            {
                return NotFound("No available products");
            }

            var existingItem = groceryItems.FirstOrDefault(x => x?.ItemName?.ToUpper() == groceryItem?.ItemName?.ToUpper());

            if (existingItem == null)
            {
                return NotFound("Product not found");
            }

            existingItem.ItemName = groceryItem.ItemName;
            existingItem.Price = groceryItem.Price;

            return Ok("Product updated successfully.");
        }


        [HttpPatch("UpdateProductPrice", Name = "UpdateProductPrice")]
        public IActionResult Patch(string groceryItemName, int? newItemPrice = null)
        { 
            if (groceryItems == null)
            {
                return NotFound("No available products!");
            }

            var existingItem = groceryItems.FirstOrDefault(x => x?.ItemName?.ToUpper() == groceryItemName?.ToUpper());

            if (existingItem == null)
            {
                return NotFound("Product not found!");
            }

            if (newItemPrice != null)
            {
                existingItem.Price = newItemPrice.Value;
            }

            return Ok("Price updated successfully.");
        }

        [HttpDelete("DeleteItems", Name = "DeleteItems")]
        public IActionResult Delete(List<int> groceryItemIds)
        {
            if (groceryItems == null)
            {
                return NotFound("No available products");
            }

            foreach (var itemId in groceryItemIds)
            {
                var itemsToRemove = groceryItems.Where(x => x?.ItemId == itemId).ToList();

                if (itemsToRemove.Count > 0)
                {
                    groceryItems.RemoveAll(x => x?.ItemId == itemId);
                }
            }
            return Ok("Items deleted successfully.");
        }

    }
}
