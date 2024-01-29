using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SDHDotNetCore.ShoppingCartMvcApp.EFDbContext;
using SDHDotNetCore.ShoppingCartMvcApp.Models;

namespace SDHDotNetCore.ShoppingCartMvcApp.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly AppDbContext _context;
        private static List<AddToCardListModel> items = new List<AddToCardListModel>();

        public ShoppingCartController(AppDbContext context)
        {
            _context = context;
        }

        [ActionName("Index")]
        public IActionResult Index()
        {
            List<ProductItemDataModel> lst = _context.ProductItems.ToList();
            if (lst is not null)
            {
                ProductItemDataResponseModel responseModel = new ProductItemDataResponseModel
                {
                    Data = lst
                };
                return View("Index", responseModel);

            }
            return NotFound("No Data Found.");
        }

        public IActionResult AddToCart()
        {
            return View(items);
        }

        [HttpPost]
        public IActionResult AddToCart(AddToCartRequestModel requestModel)
        {
            var existingItem = items.FirstOrDefault(x => x.ItemId == requestModel.ItemId);

            if (existingItem is null)
            {
                var itemProduct = _context.ProductItems.FirstOrDefault(x => x.ItemId == requestModel.ItemId);

                if (itemProduct is not null)
                {
                    items.Add(new AddToCardListModel
                    {
                        ItemId = requestModel.ItemId,
                        Name = itemProduct.Name,
                        Quantity = 1,
                        Price = itemProduct.Price
                    });
                }
            }
            else
            {
                existingItem.Quantity++;
            }

            return Ok(new { Count = items.Sum(item => item.Quantity) });
        }


        [HttpPost]
        public IActionResult RemoveFromCart(AddToCartRequestModel requestModel)
        {
            var item = items.FirstOrDefault(x => x.ItemId == requestModel.ItemId);

            if (item != null)
            {
                item.Quantity--;

                if (item.Quantity == 0)
                {
                    items.RemoveAll(x => x.ItemId == requestModel.ItemId);
                }
            }

            return Ok(new { Count = items.Sum(item => item.Quantity) });
        }

    }
}
