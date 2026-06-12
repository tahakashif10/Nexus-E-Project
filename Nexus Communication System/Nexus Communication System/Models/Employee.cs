using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexus_Communication_System.Models
{
    public class Employee
    {
        [Key]
        public int Employee_ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Employee_Name { get; set; }

        [Required]
        [StringLength(15)]
        public string Phone_Number { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string? Address { get; set; }

        [Required]
        public string Designation { get; set; }

        public decimal Salary { get; set; }

        public bool Status { get; set; }

        [NotMapped]
        public string Password { get; set; }

        [NotMapped]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}