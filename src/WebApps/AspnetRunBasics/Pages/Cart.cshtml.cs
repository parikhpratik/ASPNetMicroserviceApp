using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AspnetRunBasics.Models;
using AspnetRunBasics.Services;
using System.Linq;

namespace AspnetRunBasics
{
    public class CartModel : PageModel
    {
        private readonly IBasketService _basketService;
        public BasketModel Cart { get; set; } = new BasketModel();

        public CartModel(IBasketService basketService)
        {
            _basketService = basketService;
        }            

        public async Task<IActionResult> OnGetAsync()
        {
            Cart = await _basketService.GetBasket("pratik");            

            return Page();
        }

        public async Task<IActionResult> OnPostRemoveToCartAsync(string productId)
        {
            var userName = "pratik";

            var basket = await _basketService.GetBasket(userName);
            var relatedItem = basket.Items.FirstOrDefault(data => data.ProductId == productId);
            if(relatedItem != null)
            {
                basket.Items.Remove(relatedItem);
            }

            await _basketService.UpdateBasket(basket);

            return RedirectToPage();
        }
    }
}