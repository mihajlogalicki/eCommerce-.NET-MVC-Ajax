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
        public JsonResult Index()
        {
            List<Category> categories = _apiService.GetAllCategories();
            return Json(categories);
        }

        [HttpGet] // Template Form for adding category
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            _apiService.SaveCategory(category);
            return RedirectToAction("Index");
        }

        [HttpGet] // Template Form for Edit category
        public ActionResult Edit(int id)
        {
            var category = _apiService.GetCategoryById(id);
            return View(category);
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            _apiService.UpdateCategory(category);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var category = _apiService.GetCategoryById(id);
            return View(category);
        }

        [HttpPost]
        public ActionResult Delete(Category category)
        {
            _apiService.DeleteCategory(category);
            return RedirectToAction("Index");
        }
    }
}