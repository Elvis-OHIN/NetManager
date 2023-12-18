using System.ComponentModel.DataAnnotations;

namespace NetManager.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        public int Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsEnable { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
