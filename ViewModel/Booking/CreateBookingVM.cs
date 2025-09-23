using System.ComponentModel.DataAnnotations;

namespace resturangAPI_MVC.ViewModel.Booking
{
    public class CreateBookingVM
    {
        public int? CustomerId_FK { get; set; }

        [StringLength(100)]
        public string? Name { get; set; }

        [Phone(ErrorMessage = "Invalid Phone number")]
        [StringLength(18)]
        public string? PhoneNumber { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [StringLength(50)]
        public string? Email { get; set; }
        [Required]
        public int TableId_FK { get; set; }

        [Required]
        public int Guests { get; set; }

        [Required]
        public DateTime BookingTime { get; set; }
    }
}
