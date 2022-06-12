using System.ComponentModel.DataAnnotations;

namespace CleanDesk.Models
{
    public class StatusModel
    {
        [Key]
        public int StatusId { get; set; }
        public string? StatusName { get; set; }

        public string? RequestDetailStatusName { get; set; }
        public int RequestDetailStatusId { get; set; }
    }
}
