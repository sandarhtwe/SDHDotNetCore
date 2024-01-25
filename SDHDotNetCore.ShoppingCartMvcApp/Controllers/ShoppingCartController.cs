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
            var item = items.FirstOrDefault(x => x.ItemId == requestModel.ItemId);
            if (item is null)
            {
                items ??= new List<AddToCardListModel>();
                var itemProduct = _context.ProductItems.FirstOrDefault(x => x.ItemId == requestModel.ItemId);
                if (itemProduct is not null)
                {
                    items.Add(new AddToCardListModel
                    {
                        ItemId = requestModel.ItemId,
                        Name = itemProduct!.Name,
                        Quantity = 1,
                        Price = itemProduct!.Price
                    });
                }
            }
            else
            {
                item.Quantity++;
            }

            return Json(new { Count = items.Count });
        }

        [HttpPost]
        public IActionResult RemoveFromCart(AddToCartRequestModel requestModel)
        {
            var item = items.FirstOrDefault(x => x.ItemId == requestModel.ItemId);
            if (item is null)
                goto result;

            item.Quantity--;
            if (item.Quantity == 0)
            {
                items = items.Where(x => x.ItemId != requestModel.ItemId).ToList();
            }

        result:
            return Json(new { Count = items.Count });
        }
    }
}
