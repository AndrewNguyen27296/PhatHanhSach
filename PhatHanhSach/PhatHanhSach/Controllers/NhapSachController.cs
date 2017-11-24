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

        /**
         * Hiện danh sách phiếu nhập ở trang Views/NhapSach/Index
         */
        public ActionResult Index()
        {
            var list = entities.PHIEUNHAPs.ToList();
            return View(list);
        }

        /**
         * Hiện chi tiết của 1 phiếu nhập
         */
        public ActionResult CT_PhieuNhap(int? MaPN)
        {
            var list = entities.CT_PHIEUNHAP.Where(n => n.MaPN == MaPN);
            //var list = SachViewModel.Equals(MaPN).ToList();
            var list2 = entities.PHIEUNHAPs.Where(n => n.MaPN == MaPN);
            var model = Tuple.Create<IEnumerable<CT_PHIEUNHAP>, IEnumerable<PHIEUNHAP>>(list, list2);
            return View(model);
        }

        /**
         * Load NXB khi ở trang thêm phiếu nhập
         */
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

        /**
         *Tự động nhắc tên sách có trong CSDL
         */
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


        /**
         * Thêm các chi tiết phiếu nhập vào bảng (vào 1 session) 
         * sử dụng SachViewModel vì trong sách không có NXB 
         * Chỉ thêm vào bảng chưa lưu vào CSDL
         */
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


        /**
         * Xóa 1 phiếu nhập khỏi bảng 
         */
        public ActionResult XoaKhoiBang(int MaSach)
        {
            // Xu ly code truy xuat sach
            ((List<SachViewModel>)Session["listSach"]).RemoveAll(p => p.MaSach == MaSach);
            return RedirectToAction("NhapSach");
        }

        /**
         * Lưu bảng vào CSDL
         */
        [HttpPost]
        public ActionResult LuuCSDL(SachViewModel sachVM)
        {
                /**
                 * Lưu Ngày nhập và NXB
                 */
                PHIEUNHAP pn = new PHIEUNHAP();
                pn.NgayNhap = sachVM.NgayNhap;
                pn.MaNXB = sachVM.MaNXB;
                pn.TrangThai = true;

                var addedPN = entities.PHIEUNHAPs.Add(pn);
                entities.SaveChanges();

                int tongTien = 0;
                
                /**
                 * Lưu CT_Phiếu Nhập
                 */
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

                /**
                 * Update tồn kho
                 */
                foreach (var ton in (List<SachViewModel>)Session["listSach"])
                {
                    TONKHO tonkho = new TONKHO();
                    tonkho.MaSach = ton.MaSach;
                    tonkho.ThoiGian = sachVM.NgayNhap;
                    tonkho.SLTon = ton.SLNhap;
                    entities.TONKHOes.Add(tonkho);
                    entities.SaveChanges();
                }

                /**
                 * Update tổng tiền
                 */
                addedPN.TongTien = tongTien;

                /**
                 * Update công nợ NXB
                 */
                CONGNO_NXB cnNXB = new CONGNO_NXB();
                cnNXB.MaNXB = sachVM.MaNXB;
                cnNXB.ThoiGian = sachVM.NgayNhap;
                cnNXB.TienNo = tongTien;
                cnNXB.TienDaTra = 0;
                entities.CONGNO_NXB.Add(cnNXB);
                entities.SaveChanges();

                Session.Clear();
                return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult ThemPhieuNhap(PHIEUNHAP pn, CT_PHIEUNHAP ctpn)
        {
            entities.CT_PHIEUNHAP.Add(ctpn);
            entities.SaveChanges();
            return RedirectToAction("Index", "NhapSach");
        }
    }
}