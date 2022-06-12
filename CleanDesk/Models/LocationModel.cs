using System.ComponentModel.DataAnnotations;

namespace CleanDesk.Models
{
    public class LocationModel
    {
        [Key]
        public int LocationId { get; set; } 
        [Required]
        public string? LocationName { get; set; }
    }
}
