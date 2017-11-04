using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PhatHanhSach.Models;
using System.Web.Mvc;
using PhatHanhSach.Models.ViewModels;

namespace PhatHanhSach.Controllers
{
    public class NhapSachController : Controller
    {
        PhatHanhSachEntities entities = new PhatHanhSachEntities();

        public ActionResult Index()
        {
            var list = entities.PHIEUNHAPs.ToList();
            return View(list);
        }

        public ActionResult CT_PhieuNhap(int? MaPN)
        {
            var list = entities.CT_PHIEUNHAP.Where(n => n.MaPN == MaPN);
            //var list = SachViewModel.Equals(MaPN).ToList();
            var list2 = entities.PHIEUNHAPs.Where(n => n.MaPN == MaPN);
            var model = Tuple.Create<IEnumerable<CT_PHIEUNHAP>, IEnumerable<PHIEUNHAP>>(list, list2);
            return View(model);
        }

        public ActionResult NhapSach()
        {
            NHAXUATBAN nxb = new NHAXUATBAN();

            var getnxblist = entities.NHAXUATBANs.ToList();
            SelectList list = new SelectList(getnxblist, "MaNXB", "Ten");
            ViewBag.nxblistname = list;

            if(Session["listSach"] == null) 
                Session["listSach"] = new List<SachViewModel>();

            return View();
        }

        [HttpPost]
        public JsonResult AutoComplete(string prefix)
        {
            var sachlist = (from s in entities.SACHes
                             where s.TenSach.StartsWith(prefix)
                             select new
                             {
                                 label = s.TenSach,
                                 val = s.MaSach
                             }).ToList();

            return Json(sachlist);
        }



        [HttpPost]
        public ActionResult ThemVaoBang(SachViewModel sachVM)
        {
            var tonTaiSach = entities.SACHes.Where(x => x.TenSach == sachVM.TenSach).ToList();
            if(tonTaiSach.Count != 0)
            {
                SACH sach = entities.SACHes.Where(s => s.TenSach == sachVM.TenSach).FirstOrDefault();
                sachVM.MaSach = sach.MaSach;
                sachVM.TenSach = sach.TenSach;
                sachVM.GiaNhap = (int)sach.DonGiaNhap;
                ((List<SachViewModel>)Session["listSach"]).Add(sachVM);
                return RedirectToAction("NhapSach");   
            }
            else
            {
                TempData["ErrorMessage"] = "Trong CSDL không có tên sách này";
                return RedirectToAction("NhapSach"); 
                
            }
        }

        public ActionResult XoaKhoiBang(int MaSach)
        {
            // Xu ly code truy xuat sach
            ((List<SachViewModel>)Session["listSach"]).RemoveAll(p => p.MaSach == MaSach);
            return RedirectToAction("NhapSach");
        }

        [HttpPost]
        public ActionResult LuuCSDL(SachViewModel sachVM)
        {
            //if (sachVM.MaNXB == null)
            //{
            //    TempData["ErrorMessage2"] = "Vui Lòng Nhập Sách Trước";
            //    return RedirectToAction("NhapSach");
            //}
            //else
            //{

                PHIEUNHAP pn = new PHIEUNHAP();
                pn.NgayNhap = sachVM.NgayNhap;
                pn.MaNXB = sachVM.MaNXB;
                pn.TrangThai = true;

                var addedPN = entities.PHIEUNHAPs.Add(pn);
                entities.SaveChanges();

                int tongTien = 0;

                foreach (var ct in (List<SachViewModel>)Session["listSach"])
                {
                    int thanhTien = ct.GiaNhap * ct.SLNhap;
                    tongTien += thanhTien;
                    // Add ct phieu nhap
                    CT_PHIEUNHAP ctpn = new CT_PHIEUNHAP();
                    ctpn.MaPN = pn.MaPN;
                    ctpn.MaSach = ct.MaSach;
                    ctpn.SLNhap = ct.SLNhap;
                    ctpn.DonGia = ct.GiaNhap;
                    ctpn.ThanhTien = thanhTien;

                    entities.CT_PHIEUNHAP.Add(ctpn);
                }

                foreach (var ton in (List<SachViewModel>)Session["listSach"])
                {
                    //Update ton kho
                    TONKHO tonkho = new TONKHO();
                    tonkho.MaSach = ton.MaSach;
                    tonkho.ThoiGian = sachVM.NgayNhap;
                    tonkho.SLTon = ton.SLNhap;
                    entities.TONKHOes.Add(tonkho);
                    entities.SaveChanges();
                }

                // Update tong tien 
                addedPN.TongTien = tongTien;

                // Update cong no NXB
                CONGNO_NXB cnNXB = new CONGNO_NXB();
                cnNXB.MaNXB = sachVM.MaNXB;
                //var query = "SELECT TienNo FROM CONGNO_NXB c WHERE ThoiGian = ( SELECT max(ThoiGian) from CONGNO_NXB c where c.MaNXB = " + cnNXB.MaNXB + ")";
                //int latestDebt = Int32.Parse(query);
                cnNXB.ThoiGian = sachVM.NgayNhap;
                cnNXB.TienNo = tongTien;
                cnNXB.TienDaTra = 0;
                entities.CONGNO_NXB.Add(cnNXB);
                entities.SaveChanges();

                Session.Clear();
                return RedirectToAction("Index");
            //}
        }

        [HttpPost]
        public ActionResult ThemPhieuNhap(PHIEUNHAP pn, CT_PHIEUNHAP ctpn)
        {
            //Session["dsSach"] = new List<>();
            entities.CT_PHIEUNHAP.Add(ctpn);
                entities.SaveChanges();
            return RedirectToAction("Index", "NhapSach");
        }

        //public JsonResult KiemTraSach()
    }
}