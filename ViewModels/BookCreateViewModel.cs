using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStore.ViewModels;

public class BookCreateViewModel
{
    public int Id  { get; set; }
    public string Title { get; set; }
    
    public string Author { get; set; }
    
    [DataType(DataType.Date)]
    [Display(Name = "Date Published")]
    public DateTime DatePublished { get; set; }
    
    [Range(0, int.MaxValue, ErrorMessage = "Price must be a positive value")]
    public int Price { get; set; }
    
    public int CategoryId { get; set; }
    
    public virtual Category? Category { get; set; }

    [MaxLength(100, ErrorMessage = "Description cannot exceed 100 characters")]
    public string Description { get; set; }
    public IFormFile Image { get; set; }
    
    //Up
}