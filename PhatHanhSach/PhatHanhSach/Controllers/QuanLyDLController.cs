using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PhatHanhSach.Models;

namespace PhatHanhSach.Controllers
{
    public class QuanLyDLController : Controller
    {
        private PhatHanhSachEntities db = new PhatHanhSachEntities();

        // GET: /QuanLyDL/
        public ActionResult Index()
        {
            return View(db.DAILies.ToList());
        }

        // GET: /QuanLyDL/Details/
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DAILY daily = db.DAILies.Find(id);
            if (daily == null)
            {
                return HttpNotFound();
            }
            return View(daily);
        }

        // GET: /QuanLyDL/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /QuanLyDL/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="MaDL,Ten,DiaChi,SoDT,TrangThai")] DAILY daily)
        {
            if (ModelState.IsValid)
            {
                db.DAILies.Add(daily);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(daily);
        }

        // GET: /QuanLyDL/Edit/
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DAILY daily = db.DAILies.Find(id);
            if (daily == null)
            {
                return HttpNotFound();
            }
            return View(daily);
        }

        // POST: /QuanLyDL/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="MaDL,Ten,DiaChi,SoDT,TrangThai")] DAILY daily)
        {
            if (ModelState.IsValid)
            {
                db.Entry(daily).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(daily);
        }

        // GET: /QuanLyDL/Delete/
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DAILY daily = db.DAILies.Find(id);
            if (daily == null)
            {
                return HttpNotFound();
            }
            return View(daily);
        }

        // POST: /QuanLyDL/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DAILY daily = db.DAILies.Find(id);
            db.DAILies.Remove(daily);
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
