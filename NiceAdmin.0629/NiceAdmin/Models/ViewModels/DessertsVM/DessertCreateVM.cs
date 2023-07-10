using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NiceAdmin.Models.ViewModels
{
    public class DessertCreateVM
    {
        public int DessertId { get; set; }
        [Display(Name = "甜點名稱")]
        [Required]
        [StringLength(50)]
        public string DessertName { get; set; }
        [Display(Name = "甜點類別")]
        public int CategoryId { get; set; }
        [Display(Name = "售價")]
        [Required]
        [Range(1, 3000, ErrorMessage = "售價要在{1}元到{2}元之间")]
        public int UnitPrice { get; set; }
        [Display(Name = "描述")]
        [Required]
        [StringLength(200)]
        public string Description { get; set; }
        [Display(Name = "圖片")]
        public List<string> DessertImageName { get; set; }
        [Display(Name = "上架")]
        public bool Status { get; set; }
        [Display(Name = "預定上架日期")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? ScheduledPublishDate { get; set; }
        public DessertCreateVM()
        {
            DessertImageName = new List<string>();
        }
    }

}