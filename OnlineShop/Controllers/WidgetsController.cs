using OnlineShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShop.ApiServices;

namespace OnlineShop.Controllers
{
    public class WidgetsController : Controller
    {
        // GET: Widgets
        public ActionResult Products(bool IsLatestProducts)
        {
            WidgetViewModels WidgetVM = new WidgetViewModels();
            WidgetVM.IsLatestProduct = IsLatestProducts;

            if (IsLatestProducts)
            {
                WidgetVM.Products = ProductServices.GetSingleInstance().GetLatestProducts(4);
            } else
            {
                WidgetVM.Products = ProductServices.GetSingleInstance().GetProducts(1, 8);
            }
            return PartialView(WidgetVM);
        }
    }
}