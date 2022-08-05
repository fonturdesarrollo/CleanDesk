using System.ComponentModel.DataAnnotations;

namespace CleanDesk.Models
{
    public class RequestDetailModel
    {
        [Key]
        public int RequestDetailId { get; set; }
        public int RequestId { get; set; }
        public int EmployeeId { get; set; }
        [Required(ErrorMessage = "Son requeridas las observaciones")]
        public string? Observations { get; set; }
        [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = "Debe colocar valor mayor a cero")]
        public int Minutes { get; set; }
        public int RequestDetailStatusId { get; set; }
        public DateTime DateEnded { get; set; }
        public DateTime DateAssignated { get; set; }
        public DateTime RequestDate { get; set; }
        public string? EmployeeName { get; set; }
        public string? RequestDescription { get; set; }
        public int TechnicianId { get; set; }
        public string? TechnicianName { get; set; }
        public string? FloorNumber { get; set; }
        public string? LocationName { get; set; }
        public int LocationId { get; set; }
        public int ManagementId { get; set; }
        public string? ManagementName { get; set; }
        public string? ExtensionNumber { get; set; }
        public string? RequestDetailStatusName { get; set; }        
        public string? IPNumber { get; set; }
    }
}
