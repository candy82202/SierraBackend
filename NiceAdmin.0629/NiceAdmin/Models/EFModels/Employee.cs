namespace NiceAdmin.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Employee
    {
        public int EmployeeId { get; set; }

        [Required]
        [StringLength(50)]
        public string EmployeeName { get; set; }

        [Required]
        [StringLength(200)]
        public string EncryptedPassword { get; set; }

        public DateTime CreateAt { get; set; }
    }
}
