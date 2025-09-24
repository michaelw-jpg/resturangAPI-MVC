using System.ComponentModel.DataAnnotations;

namespace resturangAPI_MVC.ViewModel.Menu
{
    public class CreateMenuVM
    {
        [Required]
        [StringLength(50)]
        [Display(Name = "Namn")]
        public string Name { get; set; }

        [Display(Name = "Pris")]
        public int Price { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Beskrivning")]
        public string Description { get; set; }

        [Display(Name = "Populär")]
        public bool IsPopular { get; set; }

        [StringLength(2048)]
        [Display(Name = "Bild URL")]
        public string? ImageUrl { get; set; }
    }
}
