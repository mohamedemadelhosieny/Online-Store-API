namespace Store.Omda.Core.Entities.Order
{
    public class ProductItemOrder
    {

        public ProductItemOrder()
        {
            
        }
        public ProductItemOrder(int id, string name, string pictureUrl)
        {
            ProductId = id;
            ProductName = name;
            PictureURL = pictureUrl;
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string PictureURL { get; set; }
    }
}