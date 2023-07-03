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
        public static TeacherCreateVM TOCreateVM(this Teacher entiey)
        {
            return new TeacherCreateVM
            {
                TeacherId = entiey.TeacherId,
                TeacherName = entiey.TeacherName,
                TeacherImage= entiey.TeacherImage,
                Specialty = entiey.Specialty,
                Experience = entiey.Experience,
                License = entiey.License,
                Academic = entiey.Academic,
                TeacherStatus = entiey.TeacherStatus
            };
        }
        public static Teacher TOEntity(this TeacherCreateVM vm)
        {
            return new Teacher
            {
                TeacherId = vm.TeacherId,
                TeacherName = vm.TeacherName,
                TeacherImage = vm.TeacherImage,
                Specialty = vm.Specialty,
                Experience = vm.Experience,
                License = vm.License,
                Academic = vm.Academic,
                TeacherStatus = vm.TeacherStatus
            };
        }
        public static TeacherEditVM TOEditVM(this Teacher entiey)
        {
            return new TeacherEditVM
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
        public static TeacherEditImageVM TOEditImageVM(this Teacher dto)
        {
            return new TeacherEditImageVM
            {
                TeacherId = dto.TeacherId,
                TeacherImage= dto.TeacherImage
            };
        }
        public static Teacher TOEntity(this TeacherEditImageVM vm)
        {
            return new Teacher
            {
                TeacherId = vm.TeacherId,
                
                TeacherImage = vm.TeacherImage,
               
            };
        }

    }
}