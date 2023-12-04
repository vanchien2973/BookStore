using BookStore.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace BookStore.Models;

public class Cart
{
    private readonly ApplicationDbContext _context;

    public Cart(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public string Id { get; set; }
    
    public List<CartItem> CartItems { get; set; }

    public static Cart GetCart(IServiceProvider services)
    {
        ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
        var context = services.GetService<ApplicationDbContext>();
        string cartId = session.GetString("Id") ?? Guid.NewGuid().ToString();
        session.SetString("Id", cartId);
        return new Cart(context) {Id = cartId};
    }

    public CartItem GetCartItem(Book book)
    {
        return _context.CartItems.SingleOrDefault(ci => 
            ci.Id == book.Id && ci.CartId == Id);   
    }

    public void AddToCart(Book book, int quantity)
    {
        var cartItem = GetCartItem(book);
        if (cartItem == null)
        {
            cartItem = new CartItem
            {
                Book = book,
                Quantity = quantity,
                CartId = Id
            };
            _context.CartItems.Add(cartItem);
        }
        else
        {
            cartItem.Quantity += quantity;
        }

        _context.SaveChanges();
    }


    public List<CartItem> GetAllCartItems()
    {
        return CartItems ??
               (CartItems = _context.CartItems
                   .Where(ci => ci.CartId == Id)
                   .Include(ci => ci.Book)
                   .ToList());
               ;
    }

    public int GetCartTotal()
    {
        return _context.CartItems
            .Where(ci => ci.CartId == Id)
            .Select(ci => ci.Book.Price * ci.Quantity)
            .Sum();
    }
    
}