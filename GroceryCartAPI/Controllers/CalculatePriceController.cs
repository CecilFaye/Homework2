using GroceryCartAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace GroceryCartAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatePriceController : Controller
    {
        [HttpPost("CalculateTotalPrice")]
        public ActionResult<object> CalculateTotalPrice(List<GroceryCart> groceryCart)
        {
            if (groceryCart == null || groceryCart.Count == 0)
            {
                return BadRequest("The grocery cart is empty.");
            }

            double totalPrice = groceryCart.Sum(item => (item.Price ?? 0));
            double tax = totalPrice * 0.12;
            tax = Math.Round(tax, 2);

            var priceBreakdown = new
            {
                BasePrice = totalPrice,
                Tax = tax,
                TotalAmount = totalPrice + tax
            };

            return Ok(priceBreakdown);
        }
    }
}

