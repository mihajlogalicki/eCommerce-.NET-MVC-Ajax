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
        private readonly ProductServices _apiService = new ProductServices();

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