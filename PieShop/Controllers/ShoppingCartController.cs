using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PieShop.Models;
using PieShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PieShop.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly IPieRepository pieRepository;
        private readonly ShoppingCart shoppingCart;

        public ShoppingCartController(IPieRepository pieRepository,ShoppingCart shoppingCart)
        {
            this.pieRepository = pieRepository;
            this.shoppingCart = shoppingCart;
        }
        public ViewResult Cart()
        {
            var items = shoppingCart.GetShoppingCartItems();
            shoppingCart.ShoppingCartItems = items;

            var shoppingCartViewModel = new ShoppingCartViewModel
            {
                ShoppingCart = shoppingCart,
                ShoppingCartTotal = shoppingCart.GetShoppingCartTotal()
            };

            return View(shoppingCartViewModel);
        }

        public RedirectToActionResult AddToShoppingCart(int pieId)
        {
            var selectedPie = pieRepository.AllPies.FirstOrDefault(p => p.PieId == pieId);

            if (selectedPie != null)
            {
                shoppingCart.AddToCart(selectedPie, 1);
            }
            TempData["Message"] = "Successfuly added to Cart! ";
            return RedirectToAction("Cart");
        }

        public RedirectToActionResult RemoveFromShoppingCart(int pieId)
        {
            var selectedPie = pieRepository.AllPies.FirstOrDefault(p => p.PieId == pieId);

            if (selectedPie != null)
            {
                shoppingCart.RemoveFromCart(selectedPie);
            }
            return RedirectToAction("Cart");
        }
    }
}
