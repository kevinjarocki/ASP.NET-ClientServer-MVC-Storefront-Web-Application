using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaseStudy.Models
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }

        public string ProductId { get; set; }

        public string UserId { get; set; }

        public string DateCreated { get; set; }
      
        public int QtyOrdered { get; set; }
        public int QtySold { get; set; }
        public int QtyBackOrdered { get; set; }
        public decimal MSRP { get; set; }
       
        public decimal SellingPrice { get; set; }

        public decimal SubTot { get; set; }

        public decimal Extended { get; set; }
        public string Description { get; set; }
       
      
    }
}
