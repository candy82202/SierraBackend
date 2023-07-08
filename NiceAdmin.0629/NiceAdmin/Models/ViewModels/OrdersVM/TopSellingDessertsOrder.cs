using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NiceAdmin.Views.DessertOrders
{
    public class TopSellingDessertsOrder
    {
        [Display(Name ="訂單編號")]
        public int OrderID { get; set; }
      
        [Display(Name = "訂單總額")]
        public decimal TotalAmount { get; set; }
    }
}