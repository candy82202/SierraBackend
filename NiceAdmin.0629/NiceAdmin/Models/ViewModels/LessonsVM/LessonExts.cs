using NiceAdmin.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NiceAdmin.Models.ViewModels.LessonsVM {
    public static class LessonExts {

        public static LessonIndexVM ToIndexVM(this Lesson lesson)
        {
            return new LessonIndexVM
            {
                LessonId = lesson.LessonId,
                LessonTitle = lesson.LessonTitle.Trim(),
                //CategoryName=dessert.Category.CategoryName,
                LessonCategoryName = lesson.LessonCategory.LessonCategoryName, // 修改此行
                LessonInfo = lesson.LessonInfo,
                LessonDetail= lesson.LessonDetail,
                LessonPrice = lesson.LessonPrice,
                LessonStatus = lesson.LessonStatus,
                LessonHours = lesson.LessonHours,
                LessonTime = lesson.LessonTime,
                LessonDessert = lesson.LessonDessert,
                TeacherName = lesson.Teacher.TeacherName,
                MaximumCapacity = lesson.MaximumCapacity,
                CreateTime = lesson.CreateTime,

            };

        }
        public static DessertCreateVM ToCreatVM(this Dessert dessert)
        {
            return new DessertCreateVM
            {
                DessertId = dessert.DessertId,
                DessertName = dessert.DessertName.Trim(),
                CategoryId = dessert.CategoryId,
                Description = dessert.Description,
                UnitPrice = dessert.UnitPrice,
                Status = dessert.Status,

            };

        }
        public static Dessert ToEntity(this DessertCreateVM vm)
        {
            return new Dessert
            {
                DessertId = vm.DessertId,
                DessertName = vm.DessertName.Trim(),
                CategoryId = vm.CategoryId,
                Description = vm.Description,
                UnitPrice = vm.UnitPrice,
                Status = vm.Status
            };


        }
        public static DessertEditVM ToEditVM(this Dessert dessert)
        {
            return new DessertEditVM
            {
                DessertId = dessert.DessertId,
                DessertName = dessert.DessertName,
                CategoryId = dessert.CategoryId,
                Description = dessert.Description,
                UnitPrice = dessert.UnitPrice,
                Status = dessert.Status,


            };

        }
    }
}