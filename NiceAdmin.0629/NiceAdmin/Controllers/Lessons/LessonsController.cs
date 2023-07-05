using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NiceAdmin.Models.EFModels;
using NiceAdmin.Models.ViewModels;
using NiceAdmin.Models.ViewModels.LessonsVM;

namespace NiceAdmin.Controllers.Lessons {
    public class LessonsController : Controller {
        private AppDbContext db = new AppDbContext();

        // GET: Lessons
        public ActionResult Index(LessonCriteria lessonCriteria)
        {
            PrepareCategoryDataSource(lessonCriteria.LessonCategoryId);
            //ViewBag.DessertCriteria = dessertCriteria;
            ViewBag.Criteria = lessonCriteria;

            var query = db.Lessons.Include(l => l.LessonCategory);
            #region where
            if (string.IsNullOrEmpty(lessonCriteria.LessonTitle) == false)
            {
                query = query.Where(l => l.LessonTitle.Contains(lessonCriteria.LessonTitle));
            }
            if (lessonCriteria.LessonCategoryId != null && lessonCriteria.LessonCategoryId.Value > 0)
            {
                query = query.Where(l => l.LessonCategoryId == lessonCriteria.LessonCategoryId.Value);
            }
            if (lessonCriteria.MinPrice.HasValue)
            {
                query = query.Where(l => l.LessonPrice >= lessonCriteria.MinPrice.Value);
            }
            if (lessonCriteria.MaxPrice.HasValue)
            {
                query = query.Where(l => l.LessonPrice <= lessonCriteria.MaxPrice.Value);
            }
            #endregion
            var lessons = query.ToList().Select(d => d.ToIndexVM());
            return View(lessons);
        }



        private void PrepareCategoryDataSource(int? lessonCategoryId)
        {
            var categories = db.LessonCategories.ToList().Prepend(new LessonCategory());
            var teachers = db.Teachers.ToList().Prepend(new Teacher());
            ViewBag.LessonCategoryId = new SelectList(categories, "LessonCategoryId", "LessonCategoryName", lessonCategoryId);
            ViewBag.TeacherId = new SelectList(teachers, "TeacherId", "TeacherName", lessonCategoryId);

        }

        // GET: Lessons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lesson lesson = db.Lessons.Find(id);
            if (lesson == null)
            {
                return HttpNotFound();
            }
            return View(lesson);
        }

        // GET: Lessons/Create
        public ActionResult Create()
        {
            PrepareCategoryDataSource(null);
            var vm = new LessonCreateVM();
            return View(vm);
        }

        // POST: Lessons/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LessonCreateVM vm, List<HttpPostedFileBase> images)
        {
            var files = images;
            if (ModelState.IsValid && files != null)
            {
                string path = Server.MapPath("/Uploads");

                foreach (var file in files)
                {
                    string fileName = SaveUploadedFile(path, file);
                    vm.LessonImageName.Add(fileName);
                }



                Lesson lesson = vm.ToEntity();
                lesson.CreateTime = DateTime.Now;
                DateTime now = DateTime.Now;
                if (vm.LessonTime < now)
                {
                    ModelState.AddModelError("LessonTime", "開課時間不能早於現在時間");
                    PrepareCategoryDataSource(vm.LessonCategoryId);
                    return View(vm);
                }
                foreach (string imageFileName in vm.LessonImageName)
                {
                    LessonImage lessonImage = new LessonImage
                    {
                        LessonImageName = imageFileName
                    };
                    lesson.LessonImages.Add(lessonImage);
                }
                db.Lessons.Add(lesson);
                db.SaveChanges();

                if (lesson.LessonStatus) // 檢查商品是否上架
                {
                    List<Lesson> onShelfLessons = GetOnShelfLessons(lesson); // 獲取所有上架的商品
                    return RedirectToAction("Index", "Lessons", new { lessons = onShelfLessons });
                }

                return RedirectToAction("Index");
            }
            PrepareCategoryDataSource(vm.LessonCategoryId);
            return View(vm);
        }

        private List<Lesson> GetOnShelfLessons(Lesson lesson)
        {
            return db.Lessons.Where(d => d.LessonStatus).ToList();
        }

        private string SaveUploadedFile(string path, HttpPostedFileBase file1)
        {
            List<string> FileNames = new List<string>();


            //如果沒有上傳檔案或檔案室空的，就不處理，傳回string empty
            if (file1 == null || file1.ContentLength == 0) return string.Empty;
            //取得上傳檔案的附檔名
            string ext = System.IO.Path.GetExtension(file1.FileName);
            //如果附檔名不在允許的範圍哩，表示上傳不合理的檔案類型，就不處哩，傳回string.empty
            string[] allowedExts = new string[] { ".jpg", ".jpeg", ".png", ".tif" };
            if (allowedExts.Contains(ext.ToLower()) == false) return string.Empty;
            //生成一個不會重複的檔名
            string newFileName = Guid.NewGuid().ToString("N") + ext;
            string fullName = System.IO.Path.Combine(path, newFileName);
            //將上傳檔案存放到指定位置
            file1.SaveAs(fullName);
            //傳回存放的檔名
            return newFileName;
        }



        // GET: Lessons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lesson lesson = db.Lessons.Find(id);
            if (lesson == null)
            {
                return HttpNotFound();
            }

            PrepareCategoryDataSource(lesson.LessonCategoryId);
            LessonEditVM lessonEditVM = lesson.ToEditVM();
            return View(lessonEditVM);
        }

        // POST: Lessons/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LessonEditVM vm)
        {
            var lesson = db.Lessons.FirstOrDefault(l => l.LessonId == vm.LessonId);

            if (lesson == null)
            {
                return HttpNotFound();
            }

            DateTime now = DateTime.Now;
            if (vm.LessonTime < now)
            {
                ModelState.AddModelError("LessonTime", "開課時間不能早於現在時間");
                PrepareCategoryDataSource(vm.LessonCategoryId);
                return View(vm);
            }

            if (ModelState.IsValid)
            {
                var lessonInDb = db.Lessons.Find(vm.LessonId);
                if (lessonInDb == null)
                {
                    return HttpNotFound();
                }
                lessonInDb.LessonCategoryId = vm.LessonCategoryId;
                lessonInDb.LessonTitle = vm.LessonTitle;
                lessonInDb.LessonInfo = vm.LessonInfo;
                lessonInDb.LessonDetail = vm.LessonDetail;
                lessonInDb.LessonDessert = vm.LessonDessert;
                lessonInDb.LessonTime = vm.LessonTime;
                lessonInDb.LessonHours = vm.LessonHours;
                lessonInDb.MaximumCapacity = vm.MaximumCapacity;
                lessonInDb.LessonPrice = vm.LessonPrice;
                lessonInDb.LessonStatus = vm.LessonStatus;
                lessonInDb.TeacherId = vm.TeacherId;

                db.SaveChanges();

              
                return RedirectToAction("Index");
            } 
            
            PrepareCategoryDataSource(vm.LessonCategoryId);
            return View(vm);
        }

        // GET: Lessons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lesson lesson = db.Lessons.Find(id);
            if (lesson == null)
            {
                return HttpNotFound();
            }
            return View(lesson);
        }

        // POST: Lessons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Lesson lesson = db.Lessons.Find(id);
            db.Lessons.Remove(lesson);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
