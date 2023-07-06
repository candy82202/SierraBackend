using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace NiceAdmin.Models.ViewModels.TeachersVM
{
    public class UpdateTeacherStatusVM
    {
        [Key]
        public int TeacherId { get; set; }


        public string TeacherName { get; set; }
        [Display(Name = "教師姓名")]
        public string TeacherNameText
        {
            get
            {
                return this.TeacherName.Length > 3
                    ? this.TeacherName.Substring(0, 3)
                    : this.TeacherName;
            }
        }


        [StringLength(255)]
        public string TeacherImage { get; set; }


        public bool TeacherStatus { get; set; }

        [Display(Name = "是否在職")]
        public string TeacherStatusText
        {
            get
            {
                return this.TeacherStatus == true ? "在職中" : "已離職";
            }
        }
        [Display(Name = "課程名稱")]
        public string LessonTitle { get; set; }
    }
}
