using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models;

public class Book
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; }

    [MaxLength(100, ErrorMessage = "Description cannot exceed 100 characters")]
    public string Description { get; set; }

    public string Language { get; set; }

    [Required(ErrorMessage = "ISBN is required"), MaxLength(13, ErrorMessage = "ISBN should be a maximum of 13 characters")]
    [RegularExpression(@"^\d{10,13}$", ErrorMessage = "ISBN should contain only digits and be between 10 to 13 characters")]
    public string ISBN { get; set; }

    [Required(ErrorMessage = "Date Published is required")]
    [DataType(DataType.Date)]
    [Display(Name = "Date Published")]
    public DateTime DatePublished { get; set; }

    [Required(ErrorMessage = "Price is required")]
    [Range(0, int.MaxValue, ErrorMessage = "Price must be a positive value")]
    public int Price { get; set; }

    [Required(ErrorMessage = "Author is required")]
    public string Author { get; set; }
    
    [NotMapped]
    [Required(ErrorMessage = "Please select a file")]
    [Display(Name = "Choose Image")]
    public IFormFile ImagePath { get; set; }
    
    public virtual Category? Category { get; set; }
    
    
    
}