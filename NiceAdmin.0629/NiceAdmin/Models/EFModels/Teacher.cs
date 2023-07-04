namespace NiceAdmin.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Teacher
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Teacher()
        {
            Lessons = new HashSet<Lesson>();
        }

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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Lesson> Lessons { get; set; }
    }
}
