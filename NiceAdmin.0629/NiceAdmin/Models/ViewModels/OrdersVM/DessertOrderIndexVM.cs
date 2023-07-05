using NiceAdmin.Models.EFModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NiceAdmin.Models.ViewModels.OrdersVM
{
    public class DessertOrderIndexVM
    {
        [Display(Name = "訂單編號")]
        public int DessertOrderId { get; set; }
        [Display(Name = "訂購人姓名")]
        public string MemberName { get; set; }
        [Display(Name = "訂購狀態")]
        public string StatusName { get; set; }

        //public int? CouponId { get; set; }
        [Display(Name = "訂單成立時間")]
        public DateTime CreateTime { get; set; }

        [Display(Name = "收件人")]
        [Required]
        [StringLength(50)]
        public string Recipient { get; set; }

        [Display(Name = "收件人電話")]
        [Required]
        [StringLength(10)]
        public string RecipientPhone { get; set; }

        [Display(Name = "收件人地址")]
        [Required]
        [StringLength(100)]
        public string RecipientAddress { get; set; }

        [Display(Name = "運費")]
        public int ShippingFee { get; set; }

        [Display(Name = "總額")]
        public int DessertOrderTotal { get; set; }

        [Display(Name = "運送方式")]
        [Required]
        [StringLength(50)]
        public string DeliveryMethod { get; set; }

        //[StringLength(200)]
        //public string Note { get; set; }

        //[StringLength(200)]
        //public string OrderCancellationReason { get; set; }

        //[StringLength(200)]
        //public string DiscountInfo { get; set; }
        [Display(Name = "訂單詳情")]
        public List <DessertOrderDetail> DessertOrderDetails { get; set; }
    }
}