using System;
using System.Threading.Tasks;
using AspnetRunBasics.ApiCollection;
using AspnetRunBasics.ApiCollection.Interfaces;
using AspnetRunBasics.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspnetRunBasics
{
    public class CheckOutModel : PageModel
    {
        private readonly IBasketApi _basketApi;
       private readonly IOrderApi _orderApi;

        public CheckOutModel(IBasketApi basketApi, IOrderApi orderApi)
        {
            _basketApi = basketApi ?? throw new ArgumentNullException(nameof(basketApi));
           _orderApi = orderApi ?? throw new ArgumentNullException(nameof(orderApi));
        }

        [BindProperty]
        public BasketCheckoutModel Order { get; set; }

        public BasketModel Cart { get; set; } = new BasketModel();

        public async Task<IActionResult> OnGetAsync()
        {
            Cart = await _basketApi.GetBasket("string");
            return Page();
        }

        public async Task<IActionResult> OnPostCheckOutAsync()
        {
            Cart = await _basketApi.GetBasket("string");

            if (!ModelState.IsValid)
            {
                return Page();
            }
            Order.UserName = "string";
            Order.TotalPrice = Cart.TotalPrice;
            await _basketApi.Checkout(Order);
            return RedirectToPage("Confirmation", "OrderSubmitted");
        }       
    }
}