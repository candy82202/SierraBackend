using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NiceAdmin.Controllers.Promotions;
using NiceAdmin.Models.EFModels;
using NiceAdmin.Models.ViewModels.PromotionsVM;

namespace NiceAdmin.Controllers
{
    public class PromotionsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Promotions
        public ActionResult Index()
        {
            var promotions = db.Promotions.Include(p => p.Coupon).ToList();
            IEnumerable<PromotionIndexVM> vms = promotions.Select(p => p.ToIndexVM());
            return View(vms);
        }

        // GET: Promotions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Promotion promotion = db.Promotions.Find(id);
            if (promotion == null)
            {
                return HttpNotFound();
            }
            return View(promotion);
        }

        // GET: Promotions/Create
        public ActionResult Create()
        {
            PrepareCouponDataSource(null);
            return View();
        }

        // POST: Promotions/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( PromotionCreateVM vm , HttpPostedFileBase PromotionImage)
        {
            if (ModelState.IsValid)
            {
                string path = Server.MapPath("/Uploads");//檔案要存放的資料夾位置
                string fileName = SaveUploadFile(path, PromotionImage);
                vm.PromotionImage = fileName;
                var promotion = vm.ToEntity();
                db.Promotions.Add(promotion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            PrepareCouponDataSource(vm.CouponId);
            return View(vm);
        }

        // GET: Promotions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Promotion promotion = db.Promotions.Find(id);
            PromotionEditVM vm = promotion.ToEditVM();
            if (vm == null)
            {
                return HttpNotFound();
            }
            ViewBag.CouponId = new SelectList(db.Coupons, "CouponId", "CouponName", promotion.CouponId);
            return View(promotion);
        }

        // POST: Promotions/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PromotionId,CouponId,PromotionImage,PromotionName,Description,LaunchAt,StartAt,EndAt,CreateAt")] Promotion promotion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(promotion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CouponId = new SelectList(db.Coupons, "CouponId", "CouponName", promotion.CouponId);
            return View(promotion);
        }

        // GET: Promotions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Promotion promotion = db.Promotions.Find(id);
            if (promotion == null)
            {
                return HttpNotFound();
            }
            return View(promotion);
        }

        // POST: Promotions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Promotion promotion = db.Promotions.Find(id);
            db.Promotions.Remove(promotion);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        private void PrepareCouponDataSource(int? couponId)
        {
            var coupons = db.Coupons.ToList().Prepend(new Coupon());
            ViewBag.CouponId = new SelectList(coupons, "couponId", "couponName", couponId);

        }
        private string SaveUploadFile(string path, HttpPostedFileBase file1)
        {
            //如果沒上船或檔案室空的   回傳string.empty
            if (file1 == null || file1.ContentLength == 0) return string.Empty;
            //取得上傳檔案副檔名
            string ext = System.IO.Path.GetExtension(file1.FileName);//".jpg"而不是"jpg"
                                                                     //如果副檔名不再允許範圍裡,表示上船不合理的檔案類型 就不處理  回傳string.empty
            string[] allowedExts = new string[] { ".jpg", ".jpeg", ".png", ".tif" };
            if (!allowedExts.Contains(ext.ToLower())) return string.Empty;
            // 生成一個不會重複的檔名
            string newFileName = Guid.NewGuid().ToString("N") + ext; // 生成 er534263454r45636t34534sfggtwer6563462343.jpg
            string fullName = System.IO.Path.Combine(path, newFileName);

            // 將上傳檔案存放到指定位置
            file1.SaveAs(fullName);

            // 傳回存放的檔名
            return newFileName;
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
