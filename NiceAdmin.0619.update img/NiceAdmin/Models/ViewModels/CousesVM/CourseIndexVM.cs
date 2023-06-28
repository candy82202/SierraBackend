using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace NiceAdmin.Models.ViewModels.CousesVM {
    public class CourseIndexVM {

        [Display(Name = "課程分類")]
        [StringLength(50)]
        public int CourseCategoryName { get; set; }

        [Display(Name = "課程名稱")]
        [StringLength(50)]
        public string CourseTitle { get; set; }

        [Display(Name = "課程簡介")]
        [StringLength(50)]
        public string CourseInfo { get; set; }

        [Display(Name = "課程內容")]
        [StringLength(50)]
        public string CourseDetail { 
                get
                {
                    return this.CourseDetail.Length > 10
                            ? this.CourseDetail.Substring(0, 10) + "..."
                            : this.CourseDetail;
                }
            }

        [Display(Name = "課程作品")]
        [StringLength(50)]
        public string CourseDessert { get; set; }

        [Display(Name = "開課時間")]
        public DateTime CourseTime { get; set; }

        [Display(Name = "上課時數")]
        public int CourseHours { get; set; }

        [Display(Name = "報名上限")]
        public int MaximumCapacity { get; set; }

        [Display(Name = "售價")]
        [DisplayFormat(DataFormatString = "{0:#,#}")]
        public int CoursePrice { get; set; }

        public bool CourseStatus { get; set; }
        [Display(Name = "是否上架")]
        public string StatusText
        {
            get
            {
                return this.CourseStatus == true
                    ? "上架中" : "已下架";
            }
        }

        [Display(Name = "發布日期")]
        public DateTime CreateTime { get; set; }






    }
}