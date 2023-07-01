using NiceAdmin.Models.EFModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace NiceAdmin.Models.ViewModels.TeachersVM
{
    public static class TeacherExts
    {
        public static TeacherIndexVM TOIndexVM(this Teacher entiey)
        {
            return new TeacherIndexVM
            {
                TeacherId = entiey.TeacherId,
                TeacherName = entiey.TeacherName,
                Specialty = entiey.Specialty,
                Experience = entiey.Experience,
                License = entiey.License,
                Academic = entiey.Academic,
                TeacherStatus = entiey.TeacherStatus
            };
        }
    }
}