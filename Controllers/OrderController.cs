using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly Cart _cart;

        public OrderController(ApplicationDbContext context, Cart cart)
        {
            _context = context;
            _cart = cart;
        }
        
        public async Task<IActionResult> Index()
        {
            return _context.Orders != null ? 
                View(await _context.Orders.ToListAsync()) :
                Problem("Entity set 'ApplicationDbContext.Users'  is null.");
        }
        
        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Checkout(Order order, string fullName, string phoneNumber, string address)
        {
            var cartItems = _cart.GetAllCartItems();
            _cart.CartItems = cartItems;
            if (_cart.CartItems.Count == 0)
            {
                ModelState.AddModelError("", "No cart items, please add books to cart!");
            }

            if (ModelState.IsValid)
            {
                order.FullName = fullName;
                order.PhoneNumber = phoneNumber;
                order.Address = address;

                CreateOrder(order, fullName, phoneNumber, address);
                _cart.ClearCart();
                return View("CheckoutCompleted", order);
            }

            return View(order);
        }

        public IActionResult CheckoutCompleted(Order order)
        {
            return View(order);
        }

        public void CreateOrder(Order order, string fullName, string phoneNumber, string address)
        {
            order.OrderPlaced = DateTime.Now;
            order.FullName = fullName;
            order.PhoneNumber = phoneNumber;
            order.Address = address;
            var cartItems = _cart.CartItems;
            foreach (var item in cartItems)
            {
                var orderItem = new OrderItem()
                {
                    Quantity = item.Quantity,
                    BookId = item.Book.Id,
                    OrderId = order.Id,
                    Price = item.Book.Price * item.Quantity
                };
                order.OrderItems.Add(orderItem);
                order.OrderTotal += orderItem.Price;
            }
            _context.Orders.Add(order);
            _context.SaveChanges();
        }
    }
}