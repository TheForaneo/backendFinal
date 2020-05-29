using System.ComponentModel.DataAnnotations;

namespace apiweb.Models{
    public class ForgotPassword{
        [Required]
        public string Email{get;set;}
    }
}