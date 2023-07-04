using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
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
		public ActionResult Create(PromotionCreateVM vm, HttpPostedFileBase PromotionImage)
		{
			if (ModelState.IsValid)
			{
				string path = Server.MapPath("/Uploads");//檔案要存放的資料夾位置
				string fileName = SaveUploadFile(path, PromotionImage);
				vm.PromotionImage = fileName;
				vm.CouponId=vm.CouponId==0?null:vm.CouponId;
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
			Promotion promotion = db.Promotions.FirstOrDefault(p => p.PromotionId == id);
			if (promotion == null)
			{
				return HttpNotFound();
			}
			PromotionEditVM vm = promotion.ToEditVM();

			ViewBag.CouponId = new SelectList(db.Coupons, "CouponId", "CouponName", vm.CouponId);
			return View(vm);
		}

		// POST: Promotions/Edit/5
		// 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
		// 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(PromotionEditVM vm)
		{
			if (ModelState.IsValid)
			{
				var promotionInDb = db.Promotions.Find(vm.PromotionId);
				promotionInDb.PromotionName = vm.PromotionName;
				promotionInDb.CouponId = vm.CouponId==0?null: vm.CouponId;
				promotionInDb.Description = vm.Description;
				promotionInDb.LaunchAt = vm.LaunchAt;
				promotionInDb.EndAt = vm.EndAt;
				promotionInDb.StartAt = vm.StartAt;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			ViewBag.CouponId = new SelectList(db.Coupons, "CouponId", "CouponName", vm.CouponId);
			return View(vm);
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
		[HttpPost]
		public JsonResult GetCouponTime(int? couponId)
		{
			if (couponId == 0) return Json("找不到此優惠券");
			
			var coupon = db.Coupons.Find(couponId);
			if(coupon.CouponCategoryId!=2)return Json("請選擇類別為'活動'之優惠券");
			//if (coupon.EndAt < DateTime.Now) return Json("此優惠券已過期");
			if (coupon == null) return Json("找不到此優惠券");
			else
			{
                DateTime startAt =(DateTime)coupon.StartAt;
				DateTime endAt =(DateTime)coupon.EndAt;
				string starttext = startAt.ToString("yyyy-MM-dd");
				string endtext = endAt.ToString("yyyy-MM-dd");
                return Json($"{starttext},{endtext}");
			};
            
        }
		public ActionResult EditImage(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Promotion promotion = db.Promotions.FirstOrDefault(p => p.PromotionId == id);
			if (promotion == null)
			{
				return HttpNotFound();
			}
			PromotionEditImageVM vm = promotion.ToEditImageVM();

			return View(vm);
		}
		private void PrepareCouponDataSource(int? couponId)
		{
			var coupons = db.Coupons.Where(c=>c.CouponCategoryId==2)
									.ToList()
									.Prepend(new Coupon());
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
