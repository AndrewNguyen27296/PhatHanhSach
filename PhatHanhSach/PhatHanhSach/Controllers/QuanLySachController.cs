using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PhatHanhSach.Models;

namespace PhatHanhSach.Controllers
{
    public class QuanLySachController : Controller
    {
        private PhatHanhSachEntities db = new PhatHanhSachEntities();

        // GET: /QuanLySach/
        public async Task<ActionResult> Index()
        {
            var saches = db.SACHes.Include(s => s.NHAXUATBAN);
            return View(await saches.ToListAsync());
        }

        // GET: /QuanLySach/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SACH sach = await db.SACHes.FindAsync(id);
            if (sach == null)
            {
                return HttpNotFound();
            }
            return View(sach);
        }

        // GET: /QuanLySach/Create
        public ActionResult Create()
        {
            ViewBag.MaNXB = new SelectList(db.NHAXUATBANs, "MaNXB", "Ten");
            return View();
        }

        // POST: /QuanLySach/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="MaSach,MaNXB,TenSach,TacGia,LinhVuc,DonGiaNhap,DonGiaXuat,GhiChu,TrangThai")] SACH sach)
        {
            if (ModelState.IsValid)
            {
                db.SACHes.Add(sach);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.MaNXB = new SelectList(db.NHAXUATBANs, "MaNXB", "Ten", sach.MaNXB);
            return View(sach);
        }

        // GET: /QuanLySach/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SACH sach = await db.SACHes.FindAsync(id);
            if (sach == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaNXB = new SelectList(db.NHAXUATBANs, "MaNXB", "Ten", sach.MaNXB);
            return View(sach);
        }

        // POST: /QuanLySach/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="MaSach,MaNXB,TenSach,TacGia,LinhVuc,DonGiaNhap,DonGiaXuat,GhiChu,TrangThai")] SACH sach)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sach).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MaNXB = new SelectList(db.NHAXUATBANs, "MaNXB", "Ten", sach.MaNXB);
            return View(sach);
        }

        // GET: /QuanLySach/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SACH sach = await db.SACHes.FindAsync(id);
            if (sach == null)
            {
                return HttpNotFound();
            }
            return View(sach);
        }

        // POST: /QuanLySach/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SACH sach = await db.SACHes.FindAsync(id);
            db.SACHes.Remove(sach);
            await db.SaveChangesAsync();
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
