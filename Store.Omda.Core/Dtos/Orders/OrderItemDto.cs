using Store.Omda.Core.Entities.Order;

namespace Store.Omda.Core.Dtos.Orders
{
    public class OrderItemDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string PictureURL { get; set; }

        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}