using GroceryCartAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace GroceryCartAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GroceryCartController : Controller
    {

        private static List<GroceryCart>? groceryCart;

        public GroceryCartController()
        {
            InitializeList();
        }

        private void InitializeList()
        {
            if (groceryCart == null)
            {
                groceryCart = new List<GroceryCart>();
            }
        }

        [HttpGet(Name = "GetCartItems")]
        public IActionResult GetCartItems()
        {
            if (groceryCart == null)
            {
                return NotFound("No products in cart!");
            }

            var existingItem = groceryCart;

            if (existingItem != null)
            {
                return Ok(existingItem);
            }
            else
            {
                return NotFound("Product not found!");
            }
        }

        [HttpPost("AddToCart", Name = "AddToCart")]
        public IActionResult Post(GroceryCart newItem)
        {
            if (groceryCart == null)
            {
                groceryCart = new List<GroceryCart>();
            }

            if (newItem != null)
            {
                groceryCart.Add(newItem);
            }

            return Ok("Item added successfully.");
        }

        [HttpPut("UpdateCart", Name = "UpdateCart")]
        public IActionResult Put(GroceryCart editGroceryCart)
        {
            if (groceryCart == null)
            {
                return NotFound("No available products");
            }

            var existingItem = groceryCart.FirstOrDefault(x => x?.ItemName?.ToUpper() == editGroceryCart.ItemName?.ToUpper());

            if (existingItem == null)
            {
                return NotFound("Product not found");
            }

            existingItem.ItemName = editGroceryCart.ItemName;
            existingItem.Price = editGroceryCart.Price;

            return Ok("Product updated successfully.");
        }

        [HttpDelete("DeleteItemToCart", Name = "DeleteItemToCart")]
        public IActionResult Delete(GroceryCart item)
        {
            if (groceryCart == null)
            {
                return NotFound("No available products");
            }

            var itemToRemove = groceryCart.FirstOrDefault(x => AreEqual(x, item));
            if (itemToRemove != null)
            {
                groceryCart.Remove(itemToRemove);
            }
            return Ok("Items deleted successfully.");
        }

        [HttpDelete("ClearCart", Name = "ClearCart")]
        public IActionResult ClearCart()
        {
            if (groceryCart == null)
            {
                return NotFound("No available products");
            }
            groceryCart.Clear();
            return Ok("Items deleted successfully.");
        }

        private bool AreEqual(GroceryCart item1, GroceryCart item2)
        {
            if (item1 == null || item2 == null)
            {
                return false;
            }
            if (item1.ItemId != item2.ItemId)
            {
                return false;
            }
            return true;
        }
    }


}
