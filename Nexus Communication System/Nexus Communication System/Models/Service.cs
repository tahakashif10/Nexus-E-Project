using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexus_Communication_System.Models
{
    public class Service
    {
        [Key]
        public int Service_ID { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int Category_ID { get; set; }

        [ForeignKey("Category_ID")]
        public ServiceCategory? ServiceCategory { get; set; }

        [Required]
        [StringLength(100)]
        public string Service_Name { get; set; }

        public string? Description { get; set; }

        [Required]
        public decimal Service_Charge { get; set; }

        public string? Estimated_Time { get; set; }

        public string? Warranty { get; set; }

        public bool Service_Status { get; set; }
    }
}