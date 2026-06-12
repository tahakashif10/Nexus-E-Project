using System.ComponentModel.DataAnnotations;

namespace Nexus_Communication_System.Models
{
    public class ServiceCategory
    {
        [Key]
        public int Category_ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Category_Name { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }
    }
}