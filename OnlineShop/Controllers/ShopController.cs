using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using OnlineShop.ApiServices;
using OnlineShop.Entities;
using OnlineShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    //[Authorize]
    public class ShopController : Controller
    {
        private readonly ProductServices _apiService = ProductServices.GetSingleInstance();
        private readonly CategoryServices _CatApiService = new CategoryServices();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult index()
        {
            return View();
        }

        public ActionResult ProductFilter(string search, int? pageNo, int? minPrice, int? maxPrice, int? categoryId, int? sortBy)
        {
            ShopViewModel ShopVM = new ShopViewModel();
            ShopVM.Pager = pageNo.HasValue ? pageNo.Value : 1;
            ShopVM.Categories = _CatApiService.GetAllCategories();
            ShopVM.maximumPrice = _apiService.GetMaximumPrice();
            ShopVM.Products = _apiService.SearchProducts(search, ShopVM.Pager, minPrice, maxPrice, categoryId, sortBy);
            
            ShopVM.TotalProductCount = _apiService.GetProductsCount(search, categoryId, sortBy);
            ShopVM.TotalPages = (int)Math.Ceiling((decimal)ShopVM.TotalProductCount / (decimal)6);
            return PartialView(ShopVM);
        }

        [Authorize]
        public ActionResult Checkout()
        {
            ShopViewModel model = new ShopViewModel();

            var CookieCartItems = Request.Cookies["CartProducts"];
            if (CookieCartItems != null && !string.IsNullOrEmpty(CookieCartItems.Value))
            {
                model.CartProudctsIDs = CookieCartItems.Value.Split('-').Select(x => int.Parse(x)).ToList();
                model.CartProducts = _apiService.GetProductsByIDs(model.CartProudctsIDs);

                model.User = UserManager.FindById(User.Identity.GetUserId());
            }
            return View(model);
        }

        public JsonResult Ordering(string productIds)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            if (!string.IsNullOrEmpty(productIds))
            {
                var productQuantities = productIds.Split('-').Select(x => int.Parse(x)).ToList();
                var boughtProducts = _apiService.GetProductsByIDs(productQuantities.Distinct().ToList());

                Order order = new Order();
                order.UserId = User.Identity.GetUserId();
                order.OrderedAt = DateTime.Now;
                order.Status = "Pending";
                order.TotalAmount = boughtProducts.Sum(x => x.Price * productQuantities.Where(proId => proId == x.Id).Count());

                order.OrderItems = new List<OrderItem>();
                order.OrderItems.AddRange(boughtProducts.Select(x => new OrderItem() { productId = x.Id, Quantity = productQuantities.Where(proId => proId == x.Id).Count() }));
                var saveOrder = ShopService.GetSingleInstance().SaveOrder(order);

                result.Data = new { Success = true, Rows = saveOrder };
            } else
            {
                result.Data = new { Success = false };
            }

            return result;
        }
    }
}