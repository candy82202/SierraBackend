using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NiceAdmin.Models.ViewModels.TeachersVM
{
    public class TeacherCreateVM
    {
        public int TeacherId { get; set; }

        [Required]
        [StringLength(20)]
        public string TeacherName { get; set; }

        [Required]
        [StringLength(255)]
        public string TeacherImage { get; set; }

        [Required]
        [StringLength(150)]
        public string Specialty { get; set; }

        [Required]
        [StringLength(150)]
        public string Experience { get; set; }

        [Required]
        [StringLength(150)]
        public string License { get; set; }

        [Required]
        [StringLength(150)]
        public string Academic { get; set; }

        public bool TeacherStatus { get; set; }
    }
}