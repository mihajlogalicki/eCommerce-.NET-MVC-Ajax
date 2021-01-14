using OnlineShop.ApiServices;
using OnlineShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductServices _apiService = new ProductServices();

        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductTable(string search)
        {
            var products = _apiService.GetAllProducts();

            if (!string.IsNullOrEmpty(search))
            {
                products = products.Where(p => p.Name.ToLower().Contains(search)).ToList();
            }

            return PartialView(products);
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