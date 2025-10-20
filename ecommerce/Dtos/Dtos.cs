namespace ecommerce.Dtos
{
    public class CreateOrderDto
    {
        public int CustomerId { get; set; }
        public List<OrderProductDto> OrderProducts { get; set; }
    }

    public class OrderProductDto
    {
        public int ProductId { get; set; }
    }
}
