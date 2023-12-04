using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly Cart _cart;

        public CartController(ApplicationDbContext context, Cart cart)
        {
            _context = context;
            _cart = cart;
        }
        
        public IActionResult Index()
        {
            var items = _cart.GetAllCartItems();
            _cart.CartItems = items;
            return View(_cart);
        }
        
        public IActionResult AddToCart(int id)
        {
            var selectedBook = GetBookById(id);
            if (selectedBook != null)
            {
                _cart.AddToCart(selectedBook, 1);
            }

            return RedirectToAction("Index");
        }

        public Book GetBookById(int id)
        {
            return _context.Book.FirstOrDefault(b => b.Id == id);
        }
    }
}