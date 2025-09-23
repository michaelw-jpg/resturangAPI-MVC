using System.ComponentModel.DataAnnotations;

namespace resturangAPI_MVC.ViewModel.Booking
{
    public class CreateBookingVM
    {
        public int? CustomerId_FK { get; set; }

        [StringLength(100)]
        [Display(Name = "Namn")]
        public string? Name { get; set; }

        [Phone(ErrorMessage = "Invalid Phone number")]
        [StringLength(18)]
        [Display(Name = "Telefonnummer")]
        public string? PhoneNumber { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [StringLength(50)]
        [Display(Name = "Epost")]
        public string? Email { get; set; }
        [Required]
        public int TableId_FK { get; set; }

        [Required]
        [Display(Name = "Antal Gäster")]
        public int Guests { get; set; }

        [Required]
        [Display(Name = "Bokningstid")]
        public DateTime BookingTime { get; set; }
    }
}
