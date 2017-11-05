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

        // GET: /QuanLyDL/Details/5
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: /QuanLyDL/Edit/5
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

        // POST: /QuanLyDL/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: /QuanLyDL/Delete/5
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

        // POST: /QuanLyDL/Delete/5
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
