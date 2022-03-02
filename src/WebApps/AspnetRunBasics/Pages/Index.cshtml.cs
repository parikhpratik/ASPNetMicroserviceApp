using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspnetRunBasics.Models;
using AspnetRunBasics.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspnetRunBasics.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ICatalogService _catalogService;
        private readonly IBasketService _basketService;
        public IEnumerable<CatalogModel> ProductList { get; set; } = new List<CatalogModel>();

        public IndexModel(ICatalogService catalogService, IBasketService basketService)
        {
            _catalogService = catalogService;
            _basketService = basketService;            
        }        

        public async Task<IActionResult> OnGetAsync()
        {
            ProductList = await _catalogService.GetCatalog();
            return Page();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(string productId)
        {
            var product = await _catalogService.GetCatalog(productId);

            var userName = "pratik";

            var basket = await _basketService.GetBasket(userName);
            basket.Items.Add(new BasketItemModel
            {
                ProductId = productId,
                Color = "Black",
                ProductName = product.Name,
                Price = product.Price,
                Quantity = 1
            });

            await _basketService.UpdateBasket(basket);

            return RedirectToPage("Cart");
        }
    }
}
