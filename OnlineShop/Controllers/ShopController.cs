using OnlineShop.ApiServices;
using OnlineShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    [Authorize]
    public class ShopController : Controller
    {
        private readonly ProductServices _apiService = ProductServices.GetSingleInstance();
        private readonly CategoryServices _CatApiService = new CategoryServices();

        public ActionResult index()
        {
            return View();
        }

        public ActionResult ProductFilter(string search, int? pageNo, int? minPrice, int? maxPrice, int? categoryId, int? sortBy)
        {

            ShopViewModel ShopVM = new ShopViewModel();
            pageNo = pageNo.HasValue ? pageNo.Value : 1;
            ShopVM.Categories = _CatApiService.GetAllCategories();
            ShopVM.maximumPrice = _apiService.GetMaximumPrice();
            ShopVM.Products = _apiService.SearchProducts(search, pageNo.Value, minPrice, maxPrice, categoryId, sortBy);

            ShopVM.Pager = pageNo.Value;
            ShopVM.TotalProductCount = _apiService.GetProductsCount(search);
            ShopVM.TotalPages = (int)Math.Ceiling((decimal)ShopVM.TotalProductCount / (decimal)6);
            return PartialView(ShopVM);
        }

        public ActionResult Checkout()
        {
            ShopViewModel model = new ShopViewModel();

            var CookieCartItems = Request.Cookies["CartProducts"];
            if (CookieCartItems != null)
            {
                model.CartProudctsIDs = CookieCartItems.Value.Split('-').Select(x => int.Parse(x)).ToList();
                model.CartProducts = _apiService.GetProductsByIDs(model.CartProudctsIDs);
            }
            return View(model);
        }
    }
}