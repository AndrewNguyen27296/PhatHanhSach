using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhatHanhSach.Models;

namespace PhatHanhSach.Controllers
{
    public class QuanLyNXBController : Controller
    {
        PhatHanhSachEntities db = new PhatHanhSachEntities();
        // GET: QuanLyNXB
        public ActionResult Index()
        {
            var list = db.NHAXUATBANs;
            return View(list);
        }

        [HttpGet]
        public ActionResult ThemNXB()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ThemNXB(NHAXUATBAN nxb)
        {
            db.NHAXUATBANs.Add(nxb);
            db.SaveChanges();
            return RedirectToAction("Index", "QuanLyNXB");
        }
    }
}