using System.ComponentModel.DataAnnotations;

namespace resturangAPI_MVC.ViewModel.Login
{
    public class LoginVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
