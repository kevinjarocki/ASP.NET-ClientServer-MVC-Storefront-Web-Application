using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaseStudy.Models
{
    public class OrderModel
    {
        private AppDbContext _db;

        public OrderModel(AppDbContext ctx)
        {
            _db = ctx;
        }

        public int AddOrder(Dictionary<string, object> items, string user)
        {
            int orderId = -1;
            decimal total = 0M;
            using (_db)
            {
                using (var _trans = _db.Database.BeginTransaction())
                {
                    try
                    {
                        Order order = new Order();
                        Product pItem = new Product();
                        order.UserId = user;
                        order.OrderDate = System.DateTime.Now;



                        foreach (var key in items.Keys)
                        {
                            ProductViewModel item = JsonConvert.DeserializeObject<ProductViewModel>(Convert.ToString(items[key]));
                            total += item.Qty * item.MSRP;
                        }
                        total *= 1.13M;

                        order.OrderAmount = total;
                        _db.Order.Add(order);
                        _db.SaveChanges();


                        foreach (var key in items.Keys)
                        {
                            ProductViewModel item = JsonConvert.DeserializeObject<ProductViewModel>(Convert.ToString(items[key]));
                            if (item.Qty > 0)
                            {

                                OrderLineItem line = new OrderLineItem();

                                line.Product = _db.Product.FirstOrDefault(p => p.Id == item.Id);
                                line.OrderId = order.Id;

                                if (line.Product.QtyOnHand > item.Qty)
                                {
                                    line.Product.Id = item.Id;
                                    line.Product.QtyOnHand -= item.Qty;
                                    line.QtySold += item.Qty;
                                    line.QtyOrdered = item.Qty;
                                    line.SellingPrice = item.MSRP;
                                    _db.OrderLineItem.Add(line);
                                    _db.SaveChanges();
                                }
                                if (line.Product.QtyOnHand < item.Qty)
                                {
                                    line.QtyBackOrdered = (item.Qty - item.QtyOnHand);
                                    line.Product.QtyOnBackOrder += (item.Qty - line.Product.QtyOnHand);
                                    line.QtySold = item.QtyOnHand;
                                    line.SellingPrice = item.MSRP;
                                    line.Product.QtyOnHand = 0;
                                    line.QtyOrdered = item.Qty;
                                    _db.OrderLineItem.Add(line);
                                    _db.SaveChanges();
                                }
                            }
                        }
                        _trans.Commit();
                        orderId = order.Id;

                    }

                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        _trans.Rollback();
                    }
                }
            }
            return orderId;
        }
        public List<Order> GetAll(string user)
        {
            return _db.Order.Where(order => order.UserId == user).ToList<Order>();
        }
        public List<OrderViewModel> GetOrderDetails(int tid, string uid)
        {
            List<OrderViewModel> allDetails = new List<OrderViewModel>();
            // LINQ way of doing INNER JOINS
            var results = from t in _db.Set<Order>()
                          join ti in _db.Set<OrderLineItem>() on t.Id equals ti.OrderId
                          join mi in _db.Set<Product>() on ti.ProductId equals mi.Id
                          where (t.UserId == uid && t.Id == tid)
                          select new OrderViewModel
                          {
                              OrderId = ti.Id,
                              ProductId = mi.ProductName,
                              UserId = uid,
                              QtyOrdered = ti.QtyOrdered,
                              QtySold = ti.QtySold,
                              QtyBackOrdered = ti.QtyBackOrdered,
                              Description = mi.Description,
                              SellingPrice = mi.MSRP,
                              Extended = mi.MSRP * ti.QtySold,
                              DateCreated = t.OrderDate.ToString("yyyy/MM/dd - hh:mm tt"),
                              SubTot = t.OrderAmount





                          };
            allDetails = results.ToList<OrderViewModel>();
            return allDetails;
        }
    }
}

