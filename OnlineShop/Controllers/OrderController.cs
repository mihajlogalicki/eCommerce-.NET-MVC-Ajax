using Microsoft.AspNet.Identity;
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
    public class OrderController : Controller
    {
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

        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult OrderTable(string status)
        {
            OrderViewModel OrderVM = new OrderViewModel();
            OrderVM.Status = status;
            OrderVM.Orders = ShopService.GetSingleInstance().SearchOrders(status);
            return PartialView(OrderVM);
        }

        public ActionResult Details(int id)
        {
            OrderViewModel OrderVM = new OrderViewModel();
            OrderVM.Order = ShopService.GetSingleInstance().GetOrderById(id);
            if (OrderVM.Order != null)
            {
                OrderVM.OrderByUser = UserManager.FindById(OrderVM.Order.UserId);
            }

            OrderVM.AvailableStatus = new List<string>()
            {
                "Pending", "In Progress", "Delivered"
            }; 

            return View(OrderVM);
        }

        public JsonResult ChangeStatus(string status, int id)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            result.Data = new { Success = ShopService.GetSingleInstance().UpdateOrderStatus(status, id) };
            return result;
        }
    }
}