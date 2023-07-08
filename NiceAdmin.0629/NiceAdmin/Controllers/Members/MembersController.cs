using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NiceAdmin.Models.EFModels;
using NiceAdmin.Models.ViewModels.MembersVM;

namespace NiceAdmin.Controllers.Members
{
    public class MembersController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Members
        public ActionResult Index(MemberCriteria criteria)
        {
            ViewBag.Criteria = criteria;
            //var vm = db.Members.ToList().Select(e => e.ToIndexVM());

            var query = db.Members.Select(x => x);
            if (string.IsNullOrEmpty(criteria.MemberName) == false)
            {
                query = query.Where(m => m.MemberName.Contains(criteria.MemberName));
            }
            if (criteria.MinBirth.HasValue)
            {
                query = query.Where(m => m.Birth >= criteria.MinBirth);
            }
            if (criteria.MaxBirth.HasValue)
            {
                query = query.Where(m => m.Birth <= criteria.MaxBirth);
            }
            if (criteria.IsBan.HasValue)
            {
                query = query.Where(m => m.IsBan == criteria.IsBan);
            }
            var vm = query.ToList().Select(e => e.ToIndexVM());
            return View(vm);
        }

        //// GET: Members/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Members/Create
        //// 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        //// 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "MemberId,MemberName,Email,EncryptedPassword,Address,Phone,CreateAt,Birth,Gender,ImageName,IsBan,CancelTimes")] Member member)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Members.Add(member);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(member);
        //}

        //// GET: Members/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Member member = db.Members.Find(id);
        //    if (member == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(member);
        //}

        //// POST: Members/Edit/5
        //// 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        //// 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "MemberId,MemberName,Email,EncryptedPassword,Address,Phone,CreateAt,Birth,Gender,ImageName,IsBan,CancelTimes")] Member member)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(member).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(member);
        //}

        //// GET: Members/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Member member = db.Members.Find(id);
        //    if (member == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(member);
        //}

        //// POST: Members/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Member member = db.Members.Find(id);
        //    db.Members.Remove(member);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}



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
