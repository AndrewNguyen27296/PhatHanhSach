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
    public class QuanLyNXBController : Controller
    {
        private PhatHanhSachEntities db = new PhatHanhSachEntities();

        // GET: /QuanLyNXB/
        public async Task<ActionResult> Index()
        {
            return View(await db.NHAXUATBANs.ToListAsync());
        }

        // GET: /QuanLyNXB/Details/
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHAXUATBAN nhaxuatban = await db.NHAXUATBANs.FindAsync(id);
            if (nhaxuatban == null)
            {
                return HttpNotFound();
            }
            return View(nhaxuatban);
        }

        // GET: /QuanLyNXB/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /QuanLyNXB/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="MaNXB,Ten,DiaChi,SoDT,SoTK,TrangThai")] NHAXUATBAN nhaxuatban)
        {
            if (ModelState.IsValid)
            {
                db.NHAXUATBANs.Add(nhaxuatban);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(nhaxuatban);
        }

        // GET: /QuanLyNXB/Edit/
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHAXUATBAN nhaxuatban = await db.NHAXUATBANs.FindAsync(id);
            if (nhaxuatban == null)
            {
                return HttpNotFound();
            }
            return View(nhaxuatban);
        }

        // POST: /QuanLyNXB/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="MaNXB,Ten,DiaChi,SoDT,SoTK,TrangThai")] NHAXUATBAN nhaxuatban)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nhaxuatban).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(nhaxuatban);
        }

        // GET: /QuanLyNXB/Delete/
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHAXUATBAN nhaxuatban = await db.NHAXUATBANs.FindAsync(id);
            if (nhaxuatban == null)
            {
                return HttpNotFound();
            }
            return View(nhaxuatban);
        }

        // POST: /QuanLyNXB/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PHIEUXUAT phieuxuat = await db.PHIEUXUATs.FindAsync(id);
            db.PHIEUXUATs.Remove(phieuxuat);
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
