using System.ComponentModel.DataAnnotations;

namespace resturangAPI_MVC.Models
{
    public class Menu
    {
     
        public int MenuId { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Maträtt")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Pris")]
        public int Price { get; set; }

        [Required]
        [MaxLength(200)]
        [Display(Name = "Beskrivning")]
        public string Description { get; set; }

        [Required]
        public bool IsPopular { get; set; }

        [MaxLength(128)]
        [Display(Name = "Bild")]
        
        public string? ImageUrl { get; set; }
    }
}

