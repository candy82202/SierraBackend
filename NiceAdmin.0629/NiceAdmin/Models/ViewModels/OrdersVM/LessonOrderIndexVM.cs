using NiceAdmin.Models.EFModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NiceAdmin.Models.ViewModels.OrdersVM
{
    public class LessonOrderIndexVM
    {
        [Display(Name ="訂單編號")]
        public int LessonOrderId { get; set; }
        
        [Display(Name = "訂購人姓名")]
        public string MemberName { get; set; }
        [Display(Name = "訂單狀態")]
        public string StatusName { get; set; }

        //public int LessonOrderStatusId { get; set; }
        [Display(Name = "訂單狀態")]
        public int? OrderStatusId { get; set; }
        [Display(Name = "訂單成立時間")]
        public DateTime CreateTime { get; set; }
        [Display(Name = "總額")]
        public int LessonOrderTotal { get; set; }
        [Display(Name = "訂單詳情")]
        public List<LessonOrderDetail> DessertOrderDetails { get; set; }


    }
}