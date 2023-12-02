using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models;

public class Book
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; }
    
    [Required(ErrorMessage = "Author is required")]
    public string Author { get; set; }
    
    [Required(ErrorMessage = "Date Published is required")]
    [DataType(DataType.Date)]
    [Display(Name = "Date Published")]
    public DateTime DatePublished { get; set; }
    
    [Required(ErrorMessage = "Price is required")]
    [Range(0, int.MaxValue, ErrorMessage = "Price must be a positive value")]
    public int Price { get; set; }
    
    public int CategoryId { get; set; }
    
    public virtual Category? Category { get; set; }

    [MaxLength(100, ErrorMessage = "Description cannot exceed 100 characters")]
    public string Description { get; set; }
    
    [Display(Name = "Choose Image")]
    public string ImagePath { get; set; }
    
    // Up
}