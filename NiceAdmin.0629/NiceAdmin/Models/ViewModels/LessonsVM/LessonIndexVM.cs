using NiceAdmin.Models.EFModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace NiceAdmin.Models.ViewModels.LessonsVM
{
    public class LessonIndexVM
    {
        public int LessonId { get; set; }

        [Display(Name = "課程分類")]
        [StringLength(50)]
        public string LessonCategoryName { get; set; }

        [Display(Name = "課程名稱")]
        [StringLength(50)]
        public string LessonTitle { get; set; }

        [Display(Name = "課程簡介")]
        [StringLength(50)]
        public string LessonInfo { get; set; }

        public string LessonDetail { get; set; }

        [Display(Name = "課程內容")]
        public string LessonDetailText
        {
            get
            {
                return this.LessonDetail.Length > 10
                        ? this.LessonDetail.Substring(0, 10) + "..."
                        : this.LessonDetail;
            }
        }

        [Display(Name = "課程作品")]
        [StringLength(50)]
        public string LessonDessert { get; set; }

        [Display(Name = "開課時間")]
        public DateTime LessonTime { get; set; }

        [Display(Name = "上課時數")]
        public int LessonHours { get; set; }

        [Display(Name = "報名人數上限")]
        public int MaximumCapacity { get; set; }

        [Display(Name = "售價")]
        [DisplayFormat(DataFormatString = "{0:#,#}")]
        public int LessonPrice { get; set; }

        public bool LessonStatus { get; set; }
        [Display(Name = "是否上架")]
        public string StatusText
        {
            get
            {
                return this.LessonStatus == true
                    ? "上架" : "下架";
            }
        }

        [Display(Name = "發布日期")]
        public DateTime CreateTime { get; set; }

        [Display(Name = "教師姓名")]
        public string TeacherName { get; set; }
    
    }




}