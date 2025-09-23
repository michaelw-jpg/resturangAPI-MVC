using System.ComponentModel.DataAnnotations;

namespace resturangAPI_MVC.ViewModel.Menu
{
    public class PatchMenuVM
    {
        public int? MenuId { get; set; }

        [StringLength(50)]
        public string? Name { get; set; }

        
        public int? Price { get; set; }

        [StringLength(200)]
        public string? Description { get; set; }

       
        public bool IsPopular { get; set; }

        [StringLength(128)]
        public string? ImageUrl { get; set; }
    }
}
