using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace NiceAdmin.Models.ViewModels.LessonsVM {
    public class LessonCreateVM {
        public int LessonId { get; set; }
        [Required]
        [Display(Name = "課程分類")]
        [StringLength(50)]
        public string LessonCategoryName { get; set; }
        [Required]
        [Display(Name = "課程名稱")]
        [StringLength(50)]
        public string LessonTitle { get; set; }
        [Required]
        [Display(Name = "課程簡介")]
        [StringLength(50)]
        public string LessonInfo { get; set; }

        [Required]
        [Display(Name = "課程內容")]
        [StringLength(3000)]
        public string LessonDetail { get; set; }


        [Required]
        [Display(Name = "課程作品")]
        [StringLength(50)]
        public string LessonDessert { get; set; }

        [Required]
        [Display(Name = "開課時間")]
        public DateTime LessonTime { get; set; }

        [Required]
        [Display(Name = "上課時數")]
        public int LessonHours { get; set; }

        [Required]
        [Display(Name = "報名人數上限")]
        public int MaximumCapacity { get; set; }

        [Required]
        [Display(Name = "售價")]
        [DisplayFormat(DataFormatString = "{0:#,#}")]
        public int LessonPrice { get; set; }
        [Required]
        [Display(Name = "是否上架")]
        public bool LessonStatus { get; set; }


        [Required]
        [Display(Name = "教師姓名")]
        public string TeacherName { get; set; }

    }
}
