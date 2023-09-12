namespace WebApp01.Models
{
	public class CartItemModel
	{
		public long ProductId { get; set; }
		public string ProductName { get; set; } 
		public int Quantity { get; set; }
		public decimal Price { get; set; }
		public string Image { get; set; }
		public decimal Total { 
			get { return Price * Quantity; }
		}
		public CartItemModel()
		{

		}
		public CartItemModel(ProductModel product)
		{
			ProductId = product.Id;
			ProductName = product.Name;
			Price = product.Price;
			Quantity = 1;
			Image = product.Image;
		}
	}
}
