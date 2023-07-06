using NiceAdmin.Models.EFModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace NiceAdmin.Models.ViewModels.LessonsVM {
    public class LessonEditImgVM {

        public int LessonImageId { get; set; }
        public int LessonId { get; set; }

        [Display(Name = "圖片")]
        public List<string> LessonImageNames { get; set; }

        public LessonEditImgVM()
        {
            LessonImageNames = new List<string>();
        }


    }

  
}