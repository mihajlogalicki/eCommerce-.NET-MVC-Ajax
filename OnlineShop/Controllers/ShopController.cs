﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using OnlineShop.ApiServices;
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
            if (CookieCartItems != null)
            {
                model.CartProudctsIDs = CookieCartItems.Value.Split('-').Select(x => int.Parse(x)).ToList();
                model.CartProducts = _apiService.GetProductsByIDs(model.CartProudctsIDs);

                model.User = UserManager.FindById(User.Identity.GetUserId());
            }
            return View(model);
        }
    }
}