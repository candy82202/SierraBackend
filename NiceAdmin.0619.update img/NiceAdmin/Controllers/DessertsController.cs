using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using NiceAdmin.Models.EFModels;
using NiceAdmin.Models.ViewModels;

namespace NiceAdmin.Controllers
{
    public class DessertsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Desserts
        public ActionResult Index(DessertCriteria dessertCriteria)
        {
            PrepareCategoryDataSource(dessertCriteria.CategoryId);
            //ViewBag.DessertCriteria = dessertCriteria;
            ViewBag.Criteria = dessertCriteria;
            var query = db.Desserts.Include(d => d.Category);
            #region where
            if (string.IsNullOrEmpty(dessertCriteria.Name) == false)
            {
                query = query.Where(d => d.DessertName.Contains(dessertCriteria.Name));
            }
            if (dessertCriteria.CategoryId != null && dessertCriteria.CategoryId.Value > 0)
            {
                query = query.Where(d => d.CategoryId == dessertCriteria.CategoryId.Value);
            }
            if (dessertCriteria.MinPrice.HasValue)
            {
                query = query.Where(d => d.UnitPrice >= dessertCriteria.MinPrice.Value);
            }
            if (dessertCriteria.MaxPrice.HasValue)
            {
                query = query.Where(d => d.UnitPrice <= dessertCriteria.MaxPrice.Value);
            }
            #endregion
            var desserts = query.ToList().Select(d => d.ToIndexVM());
            return View(desserts);

            //var desserts = db.Desserts.Include(d => d.Category)
            //    .ToList()
            //    .Select(d=>d.ToIndexVM());
            //return View(desserts);//.ToList());
        }

        private void PrepareCategoryDataSource(int? categoryId)
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", categoryId);
        }
        // GET: Desserts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dessert dessert = db.Desserts.Find(id);
            if (dessert == null)
            {
                return HttpNotFound();
            }
            return View(dessert);
        }

        // GET: Desserts/Create
        public ActionResult Create()
        {
           // ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
           PrepareCategoryDataSource(null);
            return View();
        }

        // POST: Desserts/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(DessertCreateVM dessertCreateVM)
        //    //[Bind(Include = "DessertId,DessertName,CategoryId,UnitPrice,Description,Status,CreateTime")] Dessert dessert)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //Dessert dessert = db.Desserts.Find(id);
        //        //DessertCreateVM CreateVM = dessert.ToEditVM();
        //        //dessertCreateVM.DessertId = 
        //        Dessert dessert = dessertCreateVM.ToEntity();
        //        dessert.CreateTime = DateTime.Now;
        //        db.Desserts.Add(dessert);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    //ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", dessert.CategoryId);
        //    PrepareCategoryDataSource(dessertCreateVM.CategoryId);
        //    return View(dessertCreateVM);
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DessertCreateVM dessertCreateVM, List<HttpPostedFileBase> images)
        {
            var files = images;
            //Request.Files["file"];
            if (ModelState.IsValid && files != null)
            {

                //將file1存檔，用server.MapPath並取得最後存檔的檔案名稱
                string path = Server.MapPath("/Uploads");//檔案要存放的資料夾位置
                foreach (var file in files)
                {
                    string fileName = SaveUploadedFile(path, file);
                    dessertCreateVM.DessertImageName.Add(fileName);
                }

                //將檔名存入vm裡面
                //dessertCreateVM.Images = fileName;
                // 將檔名存入 dessertCreateVM.Images
                //vm.DessertImageName.Add(fileName);


                // 新增一筆記錄

                Dessert dessert = dessertCreateVM.ToEntity();
                dessert.CreateTime = DateTime.Now;
                // 創建 DessertImage 对象并将其与 Dessert 对象关联
                foreach (string imageFileName in dessertCreateVM.DessertImageName)
                {
                    DessertImage dessertImage = new DessertImage
                    {
                        DessertImageName = imageFileName
                    };
                    dessert.DessertImages.Add(dessertImage);
                }
                db.Desserts.Add(dessert);
                db.SaveChanges();

                if (dessert.Status) // 檢查商品是否上架
                {
                    List<Dessert> onShelfDesserts = GetOnShelfDesserts(dessert); // 獲取所有上架的商品
                    return RedirectToAction("Sierras", "Home", new { desserts = onShelfDesserts });
                }

                return RedirectToAction("Sierras");
            //} 
        }
              

            PrepareCategoryDataSource(dessertCreateVM.CategoryId);
            return View(dessertCreateVM);
        }

        private string SaveUploadedFile(string path, HttpPostedFileBase file1)
        {
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

        private List<Dessert> GetOnShelfDesserts(Dessert dessert)
        {
            return db.Desserts.Where(d => d.Status).ToList();
        }

        // GET: Desserts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dessert dessert = db.Desserts.Find(id);
            if (dessert == null)
            {
                return HttpNotFound();
            }
           // ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", dessert.CategoryId);
           DessertEditVM editVM = dessert.ToEditVM();
           
            PrepareCategoryDataSource(dessert.CategoryId);
            return View(editVM);
        }

        // POST: Desserts/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DessertEditVM vm)//[Bind(Include = "DessertId,DessertName,CategoryId,UnitPrice,Description,Status,CreateTime")] Dessert dessert)
        {
            if (ModelState.IsValid)
            {
                var dessertInDb = db.Desserts.Find(vm.DessertId);
                if (dessertInDb == null) { return HttpNotFound(); }
                dessertInDb.CategoryId = vm.CategoryId;
                dessertInDb.Status = vm.Status;
                dessertInDb.UnitPrice = vm.UnitPrice;
                dessertInDb.DessertName = vm.DessertName;
                dessertInDb.Description = vm.Description;
                //db.Entry(dessert).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", dessert.CategoryId);
            PrepareCategoryDataSource(vm.CategoryId);
            return View(vm);
        }

        // GET: Desserts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dessert dessert = db.Desserts.Find(id);
            if (dessert == null)
            {
                return HttpNotFound();
            }
            return View(dessert);
        }

        // POST: Desserts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Dessert dessert = db.Desserts.Find(id);
            db.Desserts.Remove(dessert);
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
