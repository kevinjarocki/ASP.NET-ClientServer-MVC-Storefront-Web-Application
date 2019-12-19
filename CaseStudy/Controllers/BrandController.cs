using Microsoft.AspNetCore.Mvc;
using CaseStudy.Models;
using System.Collections.Generic;
using CaseStudy.Utils;
using System;
namespace CaseStudy.Controllers
{
    public class BrandController : Controller
    {
        AppDbContext _db;
        public BrandController(AppDbContext context)
        {
            _db = context;
        }
        public IActionResult Index(BrandViewModel vm)
        {
            // only build the catalogue once
            if (HttpContext.Session.Get<List<Brand>>("brands") == null)
            {
                // no session information so let's go to the database
                try
                {
                    BrandModel brandModel = new BrandModel(_db);
                    // now load the categories
                    List<Brand> brands = brandModel.GetAll();
                    HttpContext.Session.Set<List<Brand>>("brands", brands);
                    vm.SetBrands(brands);
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Catalogue Problem - " + ex.Message;
                }
            }
            else
            {
                // no need to go back to the database as information is already in the session
                vm.SetBrands(HttpContext.Session.Get<List<Brand>>("brands"));
                ProductModel productModel = new ProductModel(_db);
                vm.Products = productModel.GetAllByBrand(vm.BrandId);
            }
            return View(vm);
        }

        public IActionResult SelectBrand(BrandViewModel vm)
        {
            BrandModel brandModel = new BrandModel(_db);
            ProductModel prodModel = new ProductModel(_db);
            List<Product> items = prodModel.GetAllByBrand(vm.BrandId);
            List<ProductViewModel> vms = new List<ProductViewModel>();
            if (items.Count > 0)
            {
                foreach (Product item in items)
                {
                    ProductViewModel mvm = new ProductViewModel();
                    mvm.Qty = 0;
                    mvm.BrandId = item.BrandId;
                    mvm.BrandName = brandModel.GetName(item.BrandId);
                    mvm.GraphicName = item.GraphicName;
                    mvm.ProductName = item.ProductName;
                    mvm.MSRP = item.MSRP;
                    mvm.QtyOnHand = item.QtyOnHand;
                    mvm.QtyOnBackOrder = item.QtyOnBackOrder;
                    mvm.Description = item.Description;
                    mvm.Id = item.Id;
                    mvm.CostPrice = item.CostPrice;
                    
                    vms.Add(mvm);
                }
                ProductViewModel[] myProduct = vms.ToArray();
                HttpContext.Session.Set<ProductViewModel[]>("product", myProduct);
            }
            vm.SetBrands(HttpContext.Session.Get<List<Brand>>("brands"));
            return View("Index", vm); // need the original Index View here
        }

        [HttpPost]
        public ActionResult SelectProduct(BrandViewModel vm)
        {
            Dictionary<string, object> cart;
            if (HttpContext.Session.Get<Dictionary<string, Object>>("cart") == null)
            {
                cart = new Dictionary<string, object>();
            }
            else
            {
                cart = HttpContext.Session.Get<Dictionary<string, object>>("cart");
            }
            ProductViewModel[] product = HttpContext.Session.Get<ProductViewModel[]>("product");
            String retMsg = "";
            foreach (ProductViewModel item in product)
            {
                if (item.Id == vm.Id)
               {
                    if (vm.Qty > 0) // update only selected item
                    {
                        item.Qty = vm.Qty;
                        retMsg = vm.Qty + " - item(s) Added!";
                        cart[item.Id] = item;
                    }
                    else
                    {
                        item.Qty = 0;
                        cart.Remove(item.Id);
                        retMsg = "item(s) Removed!";
                    }
                    vm.BrandId = item.BrandId;
                    break;
                }
            }
            ViewBag.AddMessage = retMsg;
            HttpContext.Session.Set<Dictionary<string, Object>>("cart", cart);
            vm.SetBrands(HttpContext.Session.Get<List<Brand>>("brands"));
            return View("Index", vm);
        }
    }


}
