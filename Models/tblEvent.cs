using System.ComponentModel.DataAnnotations;

namespace automationTest.Models
{
    public class tblEvent
    {
        [Key]
        public int Id { get; set; }
        public string? mail_number { get; set; }
    }
}
