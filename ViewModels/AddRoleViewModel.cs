using System.ComponentModel.DataAnnotations;

namespace BookStore.ViewModels;

public class AddRoleViewModel
{
    [Required]
    [Display (Name = "Role Name")]
    public string RoleName { get; set; }
}