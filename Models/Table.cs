using System.ComponentModel.DataAnnotations;

namespace resturangAPI_MVC.Models
{
    public class Table
    {
        public int TableId { get; set; }

        [Required]
        [Display(Name = "Bords Nummer")]
        public int TableNumber { get; set; }
        [Required]
        [Display(Name = "Antal Platser")]
        public int Seats { get; set; }
    }
}
