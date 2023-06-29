namespace NiceAdmin.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LessonOrderDetail
    {
        public int LessonOrderDetailId { get; set; }

        public int LessonOrderId { get; set; }

        public int LessonId { get; set; }

        [Required]
        [StringLength(50)]
        public string LessonTitle { get; set; }

        public int NumberOfPeople { get; set; }

        public int LessonUnitPrice { get; set; }

        public int Subtotal { get; set; }

        public virtual LessonOrder LessonOrder { get; set; }

        public virtual Lesson Lesson { get; set; }
    }
}
