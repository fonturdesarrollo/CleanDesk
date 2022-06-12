using System.ComponentModel.DataAnnotations;

namespace CleanDesk.Models
{
    public class RequestDetailModel
    {
        [Key]
        public int RequestDetailId { get; set; }
        public int RequestId { get; set; }
        public int EmployeeId { get; set; }
        public string? Observations { get; set; }
        public int Minutes { get; set; }
        public int RequestDetailStatusId { get; set; }
        public DateTime DateEnded { get; set; }
        public DateTime DateAssignated { get; set; }
        public DateTime RequestDate { get; set; }
        public string? EmployeeName { get; set; }
        public string? RequestDescription { get; set; }
        public int TechnicianId { get; set; }
        public string? TechnicianName { get; set; }
        public int FloorNumber { get; set; }
        public string? LocationName { get; set; }
        public int LocationId { get; set; }
        public string? ExtensionNumber { get; set; }
        public string? RequestDetailStatusName { get; set; }
    }
}
