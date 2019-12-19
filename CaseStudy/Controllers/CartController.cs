using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using CaseStudy.Utils;
using CaseStudy.Models;

namespace CaseStudy.Controllers
{
    public class CartController : Controller
    {

        AppDbContext _db;
        public CartController(AppDbContext context)
        {
            _db = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult ClearCart() // clear out current cart 
        {
            HttpContext.Session.Remove("cart");
            HttpContext.Session.Set<String>("Message", "Cart Cleared");
            return Redirect("/Home");
        }

        public ActionResult AddOrder()
        {
            OrderModel model = new OrderModel(_db);
            int retVal = -1;
            string retMessage = "";
            try
            {
                Dictionary<string, object> orderItems = HttpContext.Session.Get<Dictionary<string, object>>(SessionVars.Cart);
                retVal = model.AddOrder(orderItems, HttpContext.Session.GetString(SessionVars.User));
                if (retVal > 0) // order Added
                {
                    retMessage = "Order " + retVal + " Made!";
                }
                else // problem
                {
                    retMessage = "Order not created, try again later";
                }
            }
            catch (Exception ex) // big problem
            {
                retMessage = "Order was not created, try again later! - " + ex.Message;
            }
            HttpContext.Session.Remove(SessionVars.Cart); // clear out current order once persisted
            HttpContext.Session.SetString(SessionVars.Message, retMessage);
            return Redirect("/Home");
        }
    }
}