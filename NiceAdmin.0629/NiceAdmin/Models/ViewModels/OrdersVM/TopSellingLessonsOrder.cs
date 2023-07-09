using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace NiceAdmin.Models.ViewModels.OrdersVM
{
    public class TopSellingLessonsOrder
    {
      

        [Display(Name = "訂單總額")]
        public decimal TotalAmount { get; set; }
        [Display(Name = "課程標題")]
        public string LessonTitle { get; set; }
        [Display(Name = "課程編號")]
        public int LessonId { get; set; }
        [Display(Name = "人數")]
        public int TotalNumberOfPeople { get; set; }
    }
}