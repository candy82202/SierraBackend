using NiceAdmin.Models.EFModels;
using NiceAdmin.Models.ViewModels.DessertsVM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace NiceAdmin.Models.ViewModels.TeachersVM
{
    public static class TeacherExts
    {
        public static TeacherIndexVM TOIndexVM(this Teacher entiey)//ENTITY(資料庫的資料)轉成VM
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
        public static Teacher TOIndexEntity(this TeacherIndexVM vm)//VM傳回給ENTITY
        {
            return new Teacher
            {
                TeacherId = vm.TeacherId,
                TeacherName = vm.TeacherName,
                Specialty = vm.Specialty,
                Experience = vm.Experience,
                License = vm.License,
                Academic = vm.Academic,
                TeacherStatus = vm.TeacherStatus
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
            var db=new AppDbContext();
           
            var teacher= db.Teachers.FirstOrDefault(t => t.TeacherId == vm.TeacherId);
            return new Teacher
            {
                TeacherId = vm.TeacherId,
                TeacherName = teacher.TeacherName,
                TeacherImage = vm.TeacherImage,
                Specialty = teacher.Specialty,
                Experience = teacher.Experience,
                License = teacher.License,
                Academic = teacher.Academic,
                TeacherStatus = teacher.TeacherStatus
            };
        }
        public static TeacherDeleteVM TODeleteVM(this Teacher entiey)
        {
            return new TeacherDeleteVM
            {
                TeacherId = entiey.TeacherId,
                TeacherName = entiey.TeacherName,
                TeacherImage = entiey.TeacherImage,
                Specialty = entiey.Specialty,
                Experience = entiey.Experience,
                License = entiey.License,
                Academic = entiey.Academic,
                TeacherStatus = entiey.TeacherStatus
            };
        }
        public static TeacherIndexPartVM ToIndexPartVM(this Teacher teacher, DateTime createTime)
        {
            return new TeacherIndexPartVM
            {
                TeacherId = teacher.TeacherId,
                TeacherName = teacher.TeacherName,
                TeacherImage = teacher.TeacherImage,
                Specialty = teacher.Specialty,
                Experience = teacher.Experience,
                License = teacher.License,
                Academic = teacher.Academic,
                TeacherStatus = teacher.TeacherStatus,
                CreateTime = createTime
            };
        }
    }
}