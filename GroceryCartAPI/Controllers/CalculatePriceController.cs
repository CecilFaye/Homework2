using GroceryCartAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace GroceryCartAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatePriceController : Controller
    {
        [HttpPost("CalculateTotalPrice")]
        public ActionResult<object> CalculateTotalPrice(List<GroceryItem> groceryList)
        {
            if (groceryList == null || groceryList.Count == 0)
            {
                return BadRequest("The grocery list is empty.");
            }

            double totalPrice = groceryList.Sum(item => (item.Price ?? 0) * (item.Quantity ?? 0));
            double tax = totalPrice * 0.12;
            tax = Math.Round(tax, 2);

            var priceBreakdown = new
            {
                BasePrice = totalPrice,
                Tax = tax,
                Items = groceryList.Select(item => new
                {
                    ItemName = item.ItemName,
                    Quantity = item.Quantity ?? 0,
                    TotalPrice = item.Price * item.Quantity ?? 0
                }).ToList(),
                TotalAmount = totalPrice + tax
            };

            return Ok(priceBreakdown);
        }
    }
}

