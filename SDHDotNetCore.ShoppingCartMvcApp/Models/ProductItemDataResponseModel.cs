using SDHDotNetCore.ShoppingCartMvcApp.Models;

namespace SDHDotNetCore.ShoppingCartMvcApp.Models
{
    public class ProductItemDataResponseModel
    {
        public int ItemId { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public List<ProductItemDataModel> Data { get; set; }
    }
}
