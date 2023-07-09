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
using NiceAdmin.Filters;
using NiceAdmin.Models.EFModels;
using NiceAdmin.Models.Infra.EFRepositories;
using NiceAdmin.Models.Interfaces;
using NiceAdmin.Models.Services.Desserts;
using NiceAdmin.Models.ViewModels;
using NiceAdmin.Models.ViewModels.DessertsVM.ThreeLayer;
using WebGrease;

namespace NiceAdmin.Controllers
{
    [DirectToUnAuthorize(Roles = "admin,manager")]
    public class DessertsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Desserts
        [OverrideAuthorization]
        [DirectToUnAuthorize(Roles = "admin,manager,dessertSale,staff")]
        public ActionResult Index1(DessertCriteria dessertCriteria)
        {
            PrepareCategoryDataSource(dessertCriteria.CategoryId);
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

        }
        // Three Layer - Index
        [OverrideAuthorization]
        [DirectToUnAuthorize(Roles = "admin,manager,dessertSale,staff")]
        public ActionResult Index(DessertCriteria criteria)
        {
            //新增判斷甜點上架時間的方法
            UpdateScheduledDessertsStatus();
            ViewBag.Criteria = criteria;
            PrepareCategoryDataSource(criteria.CategoryId);
            IEnumerable<DessertsIndexTVM> desserts = GetDesserts(criteria);

            return View(desserts);
        }

        private IEnumerable<DessertsIndexTVM> GetDesserts(DessertCriteria criteria)
        {
            // IProductRepository repo = new ProductEFRepository();
            IDessertRepository repo = new DessertEFRepository();

            DessertService service = new DessertService(repo);
            return service.Search(criteria)
                .Select(dto => new DessertsIndexTVM
                {
                    DessertId = dto.DessertId,
                    DessertName = dto.DessertName,
                    Description = dto.Description,
                    //CategoryId=dto.CategoryId,
                    CategoryName = dto.CategoryName,
                    UnitPrice = dto.UnitPrice,
                    Status = dto.Status,
                    CreateTime = dto.CreateTime,
                });

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

                // 新增一筆記錄

                Dessert dessert = dessertCreateVM.ToEntity();
                dessert.CreateTime = DateTime.Now;
                foreach (string imageFileName in dessertCreateVM.DessertImageName)
                {
                    DessertImage dessertImage = new DessertImage
                    {
                        DessertImageName = imageFileName
                    };
                    dessert.DessertImages.Add(dessertImage);
                }

                //判斷預定上架時間
                if (dessertCreateVM.ScheduledPublishDate.HasValue)
                {
                    dessert.ScheduledPublishDate = dessertCreateVM.ScheduledPublishDate.Value;
                    dessert.Status = false; // 将状态设置为下架
                }
                else
                {
                    dessert.Status = true; // 将状态设置为上架
                }

                db.Desserts.Add(dessert);
                db.SaveChanges();

                if (dessert.Status) // 檢查商品是否上架
                {
                    List<Dessert> onShelfDesserts = GetOnShelfDesserts(dessert); // 獲取所有上架的商品
                    return RedirectToAction("Sierras", "Home", new { desserts = onShelfDesserts });
                }

                return RedirectToAction("Sierras", "Home");
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
        //public ActionResult Edit(DessertEditVM vm)//[Bind(Include = "DessertId,DessertName,CategoryId,UnitPrice,Description,Status,CreateTime")] Dessert dessert)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var dessertInDb = db.Desserts.Find(vm.DessertId);
        //        if (dessertInDb == null) { return HttpNotFound(); }
        //        dessertInDb.CategoryId = vm.CategoryId;
        //        dessertInDb.Status = vm.Status;
        //        dessertInDb.UnitPrice = vm.UnitPrice;
        //        dessertInDb.DessertName = vm.DessertName;
        //        dessertInDb.Description = vm.Description;
        //      //  dessertInDb.DessertImages = vm.DessertImageName;
        //        //db.Entry(dessert).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    //ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", dessert.CategoryId);
        //    PrepareCategoryDataSource(vm.CategoryId);
        //    return View(vm);
        //}
        public ActionResult Edit(DessertEditVM vm, List<HttpPostedFileBase> images)
        {
            //try {
            var dessert = db.Desserts.Include(d => d.DessertImages).FirstOrDefault(d => d.DessertId == vm.DessertId);
            if (dessert == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                // 更新其他属性
                dessert.CategoryId = vm.CategoryId;
                dessert.Status = vm.Status;
                dessert.UnitPrice = vm.UnitPrice;
                dessert.DessertName = vm.DessertName;
                dessert.Description = vm.Description;

                // 清除现有的图片
                dessert.DessertImages.Clear();
                // 处理上传的图片
                if (images != null && images.Count > 0)
                {
                    string path = Server.MapPath("/Uploads");
                    foreach (var file in images)
                    {
                        string fileName = SaveUploadedFile(path, file);
                        if (!string.IsNullOrEmpty(fileName))
                        {
                            dessert.DessertImages.Add(new DessertImage { DessertImageName = fileName });
                        }
                    }
                }

                // 保存更改
                db.SaveChanges();
                return RedirectToAction("Index");
            }

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
        [HttpPost]
        public ActionResult UpDesserts(List<int> dessertIds)
        {
            if (dessertIds == null || dessertIds.Count == 0)
            {
                return View("Error");
                // 如果沒有選擇任何甜點，可以在這裡給出提示或執行相應處理             
            }
            else
            {
                try
                {
                    // 根据选择的dessertIds进行上架处理
                    var dessertsToUpdate = db.Desserts.Where(d => dessertIds.Contains(d.DessertId)).ToList();
                    foreach (var dessert in dessertsToUpdate)
                    {
                        dessert.Status = true; // 上架
                    }
                    // 更新数据库
                    db.SaveChanges();
                    // 重新導向回甜點清單
                    //return RedirectToAction("Index");
                    //return Json(new { success = true });
                    return Json(new { success = true, message = "上架成功" });
                }
                catch 
                {
                    return Json(new { success = false, errorMessage = "An error occurred while marking desserts as down. Please try again later." });
                }
            }
        }
        [HttpPost]
        public ActionResult DownDesserts(List<int> dessertIds)
        {
            if (dessertIds == null || dessertIds.Count == 0)
            {
                return View("Error");
                // 如果沒有選擇任何甜點，可以在這裡給出提示或執行相應處理             
            }
            else
            {
                try
                {
                    // 根据选择的dessertIds进行下架处理
                    var dessertsToUpdate = db.Desserts.Where(d => dessertIds.Contains(d.DessertId)).ToList();
                    foreach (var dessert in dessertsToUpdate)
                    {
                        dessert.Status = false; // 下架
                    }
                    // 更新数据库
                    db.SaveChanges();
                    // 重新導向回甜點清單
                    //return RedirectToAction("Index");
                    //return Json(new { success = true });
                    return Json(new { success = true, message = "下架成功" });
                }
                catch 
                {                    
                    return Json(new { success = false, errorMessage = "An error occurred while marking desserts as down. Please try again later." });
                }
            }           
        }
        public ActionResult UpdateDessertStatus(int dessertId, bool status)
        {
            try
            {
                var dessertToUpdate = db.Desserts.Find(dessertId);
                if (dessertToUpdate == null)
                {
                    return Json(new { success = false, errorMessage = "Invalid dessert ID." });
                }

                bool previousStatus = dessertToUpdate.Status; // Get the previous status
                dessertToUpdate.Status = status; // Set status based on the parameter
                db.SaveChanges();

                string message = status ? "上架成功" : "下架成功";

                if (status && previousStatus)
                {
                    message = "此商品已上架";
                }
                else if (!status && !previousStatus)
                {
                    message = "此商品已下架";
                }

                return Json(new { success = true, message });
            }
            catch
            {
                return Json(new { success = false, errorMessage = "An error occurred while updating the dessert status. Please try again later." });
            }
        }
        public ActionResult DownDesserts()
        {
            var currentTime = DateTime.Now;
            var downDesserts = db.Desserts
      .Where(d => d.Status == false)
      .OrderByDescending(d => d.CreateTime)
      .ToList()
      .Select(d => d.ToIndexPartVM())
         .ToList();

            return PartialView("DownDesserts", downDesserts);

        }

        //新增預定上架商品時間判斷方法
        private void UpdateScheduledDessertsStatus()
        {
            var scheduledDesserts = db.Desserts.Where(d => d.ScheduledPublishDate.HasValue && d.Status == false && d.ScheduledPublishDate <= DateTime.Now).ToList();

            foreach (var dessert in scheduledDesserts)
            {
                dessert.Status = true; // Set the status to "on shelf"
                db.Entry(dessert).State = EntityState.Modified;
            }

            db.SaveChanges();
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
