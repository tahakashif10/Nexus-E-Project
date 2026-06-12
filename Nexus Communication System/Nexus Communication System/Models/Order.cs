using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexus_Communication_System.Models
{
    public class Order
    {
        [Key]
        public int Order_ID { get; set; }

        [Required]
        public int Customer_ID { get; set; }

        [ForeignKey("Customer_ID")]
        public Customer? Customer { get; set; }

        [Required]
        public int Service_ID { get; set; }

        [ForeignKey("Service_ID")]
        public Service? Service { get; set; }

        public int? Employee_ID { get; set; }

        public DateTime Order_Date { get; set; } = DateTime.Now;

        [Required]
        public string Status { get; set; } = "Pending";

        [Required]
        public string Payment_Status { get; set; } = "Pending";

        public decimal Amount { get; set; }
    }
}