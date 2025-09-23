using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace resturangAPI_MVC.Models
{
    public class Booking
    {
        [Display(Name = "BookningsNummer")]
        public int BookingId { get; set; }

        [MaxLength(100)]
        [Display (Name = "Namn")]
        public string? Name { get; set; }

        [Phone(ErrorMessage = "Invalid Phone number")]
        [MaxLength(18)]
        [Display(Name = "Telefonnummer")]
        public string? PhoneNumber { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [MaxLength(50)]
        [Display(Name = "E-post")]
        public string? Email { get; set; }

        [Required]
        [ForeignKey("Table")]
        public int TableId_FK { get; set; }

        public virtual Table? Table { get; set; }

        [Required]
        [Display (Name = "Antal Gäster")]
        public int Guests { get; set; }

        [Required]
        [Display(Name = "Bokningstid")]
        public DateTime BookingTime { get; set; }

        [Required]
        [Display(Name = "Skapad")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
