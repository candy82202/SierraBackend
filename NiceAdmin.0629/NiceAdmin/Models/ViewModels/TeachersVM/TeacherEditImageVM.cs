using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace NiceAdmin.Models.ViewModels.TeachersVM
{
    public class TeacherEditImageVM
    {
        public int TeacherId { get; set; }

        [Display(Name = "教師照片")]

        [StringLength(255)]
        public string TeacherImage { get; set; }
    }
}