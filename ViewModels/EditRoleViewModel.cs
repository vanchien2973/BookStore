using System.ComponentModel.DataAnnotations;

namespace BookStore.ViewModels;

public class EditRoleViewModel
{
    public string Id { get; set; }
    
    [Display(Name = "Role Name")]
    [Required(ErrorMessage = "Role name is required")]
    public string RoleName { get; set; }
    public List<string> Users { get; set; }

}