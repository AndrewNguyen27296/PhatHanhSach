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
    
    public partial class NGUOIDUNG
    {
        public int MaND { get; set; }
        public string HoTen { get; set; }
        public string Pass { get; set; }
        public string Email { get; set; }
        public string SoDT { get; set; }
        public Nullable<int> MaCV { get; set; }
        public Nullable<bool> TrangThai { get; set; }
    
        public virtual CHUCVU CHUCVU { get; set; }
    }
}