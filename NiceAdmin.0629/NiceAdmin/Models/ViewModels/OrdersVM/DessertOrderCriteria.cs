using NiceAdmin.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NiceAdmin.Models.ViewModels.OrdersVM
{
    public class DessertOrderCriteria
    {
        public string MemberName { get; set; }
       
        public int? OrderStatusId { get; set; }
        public string Recipient { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        
    }
}