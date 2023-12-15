namespace BookStore.Models;

public class Order
{
    public int Id { get; set; }
    
    public string FullName { get; set; }
    
    public string PhoneNumber { get; set; }
    
    public string Address { get; set; }

    public List<OrderItem> OrderItems { get; set; } = new();
    
    public int OrderTotal { get; set; }
    
    public DateTime OrderPlaced { get; set; }
    
}