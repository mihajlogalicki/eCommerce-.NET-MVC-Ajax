using OnlineShop.ApiServices;
using OnlineShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class ShopController : Controller
    {
        private readonly ProductServices _apiService = ProductServices.GetSingleInstance();
        private readonly CategoryServices _CatApiService = new CategoryServices();

        public ActionResult index(string search, int? minPrice, int? maxPrice, int? categoryId, int? sortBy)
        {
            ShopViewModel ShopVM = new ShopViewModel();

            ShopVM.Categories = _CatApiService.GetAllCategories();
            ShopVM.maximumPrice = _apiService.GetMaximumPrice();
            ShopVM.Products = _apiService.SearchProducts(search, minPrice, maxPrice, categoryId, sortBy);
            return View(ShopVM);
        }

        public ActionResult ProductFilter(string search, int? minPrice, int? maxPrice, int? categoryId, int? sortBy)
        {
            ShopViewModel ShopVM = new ShopViewModel();

            ShopVM.Products = _apiService.SearchProducts(search, minPrice, maxPrice, categoryId, sortBy);
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