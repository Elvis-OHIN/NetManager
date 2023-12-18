using System.ComponentModel.DataAnnotations;

namespace NetManager.Models
{
    public class Logs
    {
        [Key]
        public int Id { get; set; }
        public string Action { get; set; }
        public string SqlRequest { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
