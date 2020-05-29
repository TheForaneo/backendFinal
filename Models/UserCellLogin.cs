using System.ComponentModel.DataAnnotations;

namespace apiweb.Models{
    public class UserCellLogin{
    [Required]
    public string Cellphone { get; set; }
 
    [Required]
    public string Password { get; set; }
    
    }
}