using OnlineShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShop.ApiServices;

namespace OnlineShop.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryServices _apiService = new CategoryServices(); 

        [HttpGet]
        public ActionResult Index()
        {
            List<Category> categories = _apiService.GetAllCategories();
            return View(categories);
        }

        [HttpGet] // Template Form for adding category
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            _apiService.CreateCategory(category);
            return RedirectToAction("Index");
        }
    }
}