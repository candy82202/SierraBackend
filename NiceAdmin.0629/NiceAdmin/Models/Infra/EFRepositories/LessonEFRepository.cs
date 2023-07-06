using NiceAdmin.Models.DTOs.Lessons;
using NiceAdmin.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace NiceAdmin.Models.Infra.EFRepositories {
    public class LessonEFRepository {

        private readonly AppDbContext _db;
        
        public LessonEFRepository(AppDbContext db)
        {
            _db = new AppDbContext();
        }

        //public void UpdateImage(int id, string fileNames) {
        //    var lesson = _db.Lessons.FirstOrDefault(l => l.LessonId == id);
        //    if (lesson == null) { return; }


        //    lesson.LessonImages.LessonImageName = fileNames;
        //    _db.SaveChanges();
        //}

        //public LessonDto GetById(int id)
        //{
        //    var lesson = _db.Lessons.Include(l => l.LessonCategory).FirstOrDefault(l => l.LessonId == id);
        //    return lesson?.ToDto();
        //}

    }
}