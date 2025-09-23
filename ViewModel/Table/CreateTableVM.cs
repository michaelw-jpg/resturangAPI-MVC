using System.ComponentModel.DataAnnotations;

namespace resturangAPI_MVC.ViewModel.Table
{
    public class CreateTableVM
    {
        [Required]
        public int TableNumber { get; set; }
        [Required]
        public int Seats { get; set; }
    }
}
