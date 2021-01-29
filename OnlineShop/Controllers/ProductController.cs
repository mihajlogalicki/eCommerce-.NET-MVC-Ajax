﻿using OnlineShop.ApiServices;
using OnlineShop.Entities;
using OnlineShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductServices _apiService = ProductServices.GetSingleInstance();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductTable(string search, int? PageNo)
        {
            ProductViewModel model = new ProductViewModel();
            model.SearchTerm = search;

            model.PageNo = PageNo.HasValue ? PageNo.Value : 1;
            model.Products = _apiService.GetAllProducts(model.SearchTerm, model.PageNo);
           
            // Total Pages
            int pageSize = 3;
            var productCount = _apiService.GetProductsCount(model.SearchTerm);
            model.TotalPages = (int)Math.Ceiling((decimal)productCount / (decimal)pageSize);
            return PartialView(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            CategoryServices serviceCategory = new CategoryServices();
            var categories = serviceCategory.GetAllCategories();
            return PartialView(categories);
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            _apiService.SaveProduct(product);
            return RedirectToAction("ProductTable");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var product = _apiService.GetProductById(id);
            return PartialView(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            _apiService.UpdateProduct(product);
            return RedirectToAction("ProductTable");
        }

        [HttpPost]
        public ActionResult Delete(Product product)
        {
            _apiService.DeleteProduct(product);
            return RedirectToAction("ProductTable");
        }
    }
}