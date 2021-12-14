using System;
using System.Linq;
using System.Threading.Tasks;
using AspnetRunBasics.ApiCollection.Interfaces;
using AspnetRunBasics.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspnetRunBasics
{
    public class CartModel : PageModel
    {
        private readonly IBasketApi _basketApi;

        public CartModel(IBasketApi cartRepository)
        {
            _basketApi = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
        }

        public BasketModel Cart { get; set; } = new BasketModel();        

        public async Task<IActionResult> OnGetAsync()
        {
            Cart = await _basketApi.GetBasket("string");            

            return Page();
        }

        public async Task<IActionResult> OnPostRemoveToCartAsync(string ProductId)
        {
            //  await _basketApi.RemoveItem(cartId, cartItemId);
            var basket = await _basketApi.GetBasket("string");
            var item =basket.Items.FirstOrDefault(i=>i.ProductId== ProductId);
            basket.Items.Remove(item);
            await _basketApi.CreateBasket(basket);
            return RedirectToPage();
        }
    }
}