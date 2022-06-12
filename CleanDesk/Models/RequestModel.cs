using System.ComponentModel.DataAnnotations;

namespace CleanDesk.Models
{
    public class RequestModel
    {
        [Key]
        public int RequestId { get; set; }
        public int EmployeeId { get; set; }
        [Required(ErrorMessage = "Es requerida la descripción del problema")]
        public string? RequestDescription { get; set; }
        public string? EmployeeName { get; set; }
        public int RequestAreaDetailId { get; set; }
        public string? RequestAreaName { get; set; }
        public int RequestStatusId { get; set; }
        public string? RequestStatusName { get; set; }
        public DateTime RequestEnd { get; set; }
        public DateTime RequestDate { get; set; }
        public int LocationId { get; set; }
        public string? LocationName { get; set; }
        public int FloorId { get; set; }
        public int FloorNumber { get; set; }
        public string? ExtensionNumber { get; set; }
    }
}
