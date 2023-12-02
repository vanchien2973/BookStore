using System.ComponentModel.DataAnnotations;

namespace BookStore.Models;

public class Category
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Category name is required")]
    public string Name { get; set; }
    
    public virtual ICollection<Book>? Book { get; set; }
    //Up
}