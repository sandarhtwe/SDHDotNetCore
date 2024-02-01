using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SDHDotNetCore.ShoppingCartMvcApp.Models
{
    [Table("Tbl_item")]
    public class ProductItemDataModel
    {
        [Key]
        public int ItemId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public class AddToCartRequestModel
    {
        public int ItemId { get; set; }
    }

    public class AddToCardListModel
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

    public class ThankYouViewModel
    {
        public decimal TotalAmount { get; set; }
        public List<AddToCardListModel> PurchasedItems { get; set; }
    }
}
