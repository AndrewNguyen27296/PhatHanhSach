//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PhatHanhSach.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SACH
    {
        public SACH()
        {
            this.CT_BAOCAODL = new HashSet<CT_BAOCAODL>();
            this.CT_DOANHSO = new HashSet<CT_DOANHSO>();
            this.CT_PHIEUNHAP = new HashSet<CT_PHIEUNHAP>();
            this.CT_PHIEUXUAT = new HashSet<CT_PHIEUXUAT>();
            this.TONKHOes = new HashSet<TONKHO>();
            this.TONKHODLs = new HashSet<TONKHODL>();
        }
    
        public int MaSach { get; set; }
        public int MaNXB { get; set; }
        public string TenSach { get; set; }
        public string TacGia { get; set; }
        public string LinhVuc { get; set; }
        public Nullable<int> DonGiaNhap { get; set; }
        public Nullable<int> DonGiaXuat { get; set; }
        public string GhiChu { get; set; }
        public Nullable<bool> TrangThai { get; set; }
    
        public virtual ICollection<CT_BAOCAODL> CT_BAOCAODL { get; set; }
        public virtual ICollection<CT_DOANHSO> CT_DOANHSO { get; set; }
        public virtual ICollection<CT_PHIEUNHAP> CT_PHIEUNHAP { get; set; }
        public virtual ICollection<CT_PHIEUXUAT> CT_PHIEUXUAT { get; set; }
        public virtual NHAXUATBAN NHAXUATBAN { get; set; }
        public virtual ICollection<TONKHO> TONKHOes { get; set; }
        public virtual ICollection<TONKHODL> TONKHODLs { get; set; }
    }
}
