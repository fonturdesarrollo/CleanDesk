using System.ComponentModel.DataAnnotations;

namespace CleanDesk.Models
{
    public class GroupModel
    {
        [Key]
        public int SecurityGroupId { get; set; }
        public string? SecurityGroupName { get; set; }
        public string? SecurityGroupDescription { get; set; }
    }
}
