using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace NiceAdmin.Models.DTOs
{
    public class DessertOrderIndexDto
    {

        public int DessertOrderId { get; set; }

        //public int MemberId { get; set; }

        //public int DessertOrderStatusId { get; set; }

        public string MemberName { get; set; }
        public string StatusName { get; set; }

        public int? CouponId { get; set; }

        public DateTime CreateTime { get; set; }

    
        public string Recipient { get; set; }

   
        public string RecipientPhone { get; set; }

        public string RecipientAddress { get; set; }

        public int ShippingFee { get; set; }

        public int DessertOrderTotal { get; set; }

        public string DeliveryMethod { get; set; }

  
        public string Note { get; set; }

   
        public string OrderCancellationReason { get; set; }

        public string DiscountInfo { get; set; }
    }
}