using System;
using System.Threading.Tasks;
using AspnetRunBasics.ApiCollection.Interfaces;
using AspnetRunBasics.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspnetRunBasics
{
    public class ProductDetailModel : PageModel
    {
        private readonly ICatalogApi _catalogApi;
        private readonly IBasketApi _basketApi;

        public ProductDetailModel(ICatalogApi productRepository, IBasketApi cartRepository)
        {
            _catalogApi = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _basketApi = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
        }

        public CatalogModel Product { get; set; }

        [BindProperty]
        public string Color { get; set; }

        [BindProperty]
        public int Quantity { get; set; }

        public async Task<IActionResult> OnGetAsync(string? productId)
        {
            if (productId == null)
            {
                return NotFound();
            }

            Product = await _catalogApi.GetCtalog(productId);
            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(string productId,int quantity)
        {
            //if (!User.Identity.IsAuthenticated)
            //    return RedirectToPage("./Account/Login", new { area = "Identity" });
            var product = await _catalogApi.GetCtalog(productId);
            var username = "string";
            var basket = await _basketApi.GetBasket(username);
            basket.Items.Add(
                new BasketItemModel
                {
                    ProductId = product.Id,
                    Color = "red",
                    Price = product.Price,
                    ProductName = product.Name,
                    Quantity = quantity
                }
                );
            var updatedBasket = _basketApi.CreateBasket(basket);
            return RedirectToPage("Cart");
        }
    }
}