using System.ComponentModel.DataAnnotations;

namespace resturangAPI_MVC.ViewModel.Table
{
    public class PatchTableVM
    {
        [Display(Name = "Bords Nummer")]
        public int? TableNumber { get; set; }
        [Display(Name = "Antal Platser")]
        public int? Seats { get; set; }
    }
}
