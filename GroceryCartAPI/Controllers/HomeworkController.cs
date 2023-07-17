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
                    "Apple", "Banana", "Carrot", "Lemon", "Strawberry",
                    "Grapes", "Watermelon", "Pineapple", "Kiwi", "Peach",
                    "Lettuce", "Tomato", "Onion", "Garlic", "Eggplant"
                };

                Random random = new Random();

                for (int i = 1; i <= 15; i++)
                {
                   
                    var item = new GroceryItem
                    {
                        ItemId = i,
                        ItemName = items[i - 1],
                        ImageUrl = "item" + items[i - 1].ToLower() + ".png",
                        Price = random.Next(10, 100),
                    };

                    groceryItems.Add(item);
                }
            }
        }

        [HttpGet(Name = "GetProducts")]
        public IActionResult GetProducts()
        {
            if (groceryItems == null)
            {
                return NotFound("Product not found!");
            }

            var existingItem = groceryItems;

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
