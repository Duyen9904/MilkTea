using System.ComponentModel.DataAnnotations;

namespace PRN222.Assignment.Blazor.Client.Components.Models.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
