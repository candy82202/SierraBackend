using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace NiceAdmin.Models.ViewModels.OrdersVM
{
    public class TopSellingLessonsVM
    {
        [Display(Name = "人數")]
        public int NumberOfPeople { get; set; }
        [Display(Name = "課程名稱")]
        public string LessonTitle { get; set; }
    }
}