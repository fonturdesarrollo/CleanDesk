using System.ComponentModel.DataAnnotations;

namespace CleanDesk.Models
{
    public class RequestAreaModel
    {
        [Key]
        public int RequestAreaId { get; set; }
        public string? RequestAreaName { get; set; }
    }
}
