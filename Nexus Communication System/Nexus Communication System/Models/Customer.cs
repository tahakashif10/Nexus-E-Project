using System.ComponentModel.DataAnnotations;

namespace Nexus_Communication_System.Models
{
    public class Customer
    {
        [Key]
        public int Customer_ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Customer_Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(15)]
        public string Phone_Number { get; set; }

        public string? Address { get; set; }

        public bool Status { get; set; }
    }
}