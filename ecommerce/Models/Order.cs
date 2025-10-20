using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecommerce.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public string Status { get; set; } = "Pending";

        public double TotalPrice { get; set; }
        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
