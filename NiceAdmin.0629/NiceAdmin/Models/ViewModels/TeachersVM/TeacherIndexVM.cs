using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NiceAdmin.Models.ViewModels.TeachersVM
{
    public class TeacherIndexVM
    {
        public int TeacherId { get; set; }

       
        public string TeacherName { get; set; }
        [Display(Name = "教師姓名")]
        public string TeacherNameText 
        {
            get
            {
                return this.TeacherName.Length>3
                    ?this.TeacherName.Substring(0,3)
                    :this.TeacherName;
            }
        }

        //[Required]
        //[StringLength(255)]
        //public string TeacherImage { get; set; }


        public string Specialty { get; set; }
        [Display(Name = "專長")]
        public string SpecialtyText
        {
            get
            {
                return this.Specialty.Length>6
                    ? this.Specialty.Substring(0,6)+"..."
                    : this.Specialty;
            }
        }

        
        public string Experience { get; set; }
        [Display(Name = "經歷")]
        public string ExperienceText 
        {
            get
            {
                return this.Experience.Length>10
                    ?this.Experience.Substring(0,10)+"..."
                    :this.Experience;
            }
        }



        public string License { get; set; }
        [Display(Name = "證照")]
        public string LicenseText 
        {
            get
            {
                return this.License.Length>7
                    ? this.License.Substring(0,7)+"..."
                    :this.License;
            }
        }

        
        public string Academic { get; set; }
        [Display(Name = "學歷")]
        public string AcademicText 
        {
            get 
            {
                return this.Academic.Length > 10
                    ? this.Academic.Substring(0, 10) + "..."
                    : this.Academic;
            }
        }

      
        public bool TeacherStatus { get; set; }

        [Display(Name = "是否上架")]
        public string TeacherStatusText {
            get
            {
                return this.TeacherStatus == true ? "上架" : "下架";
            }
                }
    }
}