using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PieShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PieShop.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly ShoppingCart shoppingCart;
        private readonly IOrderRepository orderRepository;

        public OrderController(IOrderRepository orderRepository,ShoppingCart shoppingCart)
        {
            this.shoppingCart = shoppingCart;
            this.orderRepository = orderRepository;
        }

        public IActionResult Checkout()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            var items = shoppingCart.GetShoppingCartItems();
            shoppingCart.ShoppingCartItems = items;

            if (shoppingCart.ShoppingCartItems.Count == 0)
            {
                ModelState.AddModelError("", "Your cart is empty, add some pies first");
            }

            if (ModelState.IsValid)
            {
                orderRepository.CreateOrder(order);
                shoppingCart.ClearCart();
                return RedirectToAction("CheckoutComplete");
            }
            return View(order);
        }

        public IActionResult CheckoutComplete()
        {
            ViewBag.CheckoutMessage = "Thanks for ordering from our shop, your order is being processed!";

            return View();
        }
    }
}
