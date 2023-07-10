using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NiceAdmin.Views.DessertOrders
{
    public class TopSellingDessertsOrder
    {

        [Display(Name = "訂單總額")]
        public decimal TotalAmount { get; set; }
        [Display(Name = "甜點名稱")]
        public string DessertName { get; set; }
        [Display(Name = "甜點編號")]
        public int DessertId { get; set; }
        [Display(Name = "總數量")]
        public int TotalQuantity { get; set; }
    }
}