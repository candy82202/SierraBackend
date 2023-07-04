using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace NiceAdmin.Models.ViewModels.TeachersVM
{
    public class TeacherDeleteVM
    {
        public int TeacherId { get; set; }

        [Display(Name = "教師姓名")]
        [Required]
        [StringLength(20)]
        public string TeacherName { get; set; }

        [Display(Name = "教師照片")]

        [StringLength(255)]
        public string TeacherImage { get; set; }

        [Display(Name = "專長")]
        [Required]
        [StringLength(150)]
        public string Specialty { get; set; }

        [Display(Name = "經歷")]
        [Required]
        [StringLength(150)]
        public string Experience { get; set; }

        [Display(Name = "證照")]
        [Required]
        [StringLength(150)]
        public string License { get; set; }

        [Display(Name = "學歷")]
        [Required]
        [StringLength(150)]
        public string Academic { get; set; }

        [Display(Name = "是否上架")]
        public bool TeacherStatus { get; set; }
    }
}