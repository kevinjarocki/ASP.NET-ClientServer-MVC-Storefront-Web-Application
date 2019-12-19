using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaseStudy.Models;
using CaseStudy.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CaseStudy.Controllers
{
    public class OrderController : Controller
    {

        AppDbContext _db;
        public OrderController(AppDbContext context)
        {
            _db = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult ClearOrder() // clear out current cart 
        {
            HttpContext.Session.Remove("cart");
            HttpContext.Session.Set<String>("Message", "Order Cleared");
            return Redirect("/Home");
        }

        [Route("[action]")]
        public IActionResult GetOrders()
        {
            OrderModel model = new OrderModel(_db);
            return Ok(model.GetAll(HttpContext.Session.GetString(SessionVars.User)));
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

        public IActionResult List()
        {
            // they can't list orders if they're not logged on
            if (HttpContext.Session.GetString(SessionVars.User) == null)
            {
                return Redirect("/Login");
            }
            return View("List");
        }

        [Route("[action]/{tid:int}")]
        public IActionResult GetOrderDetails(int tid)
        {
            OrderModel model = new OrderModel(_db);
            return Ok(model.GetOrderDetails(tid, HttpContext.Session.GetString(SessionVars.User)));
        }
    }
}

