using System;
using System.Collections.Generic;

namespace MobileStore_Project.Models
{
    public partial class SanPham
    {
        public SanPham()
        {
            DonHangs = new HashSet<DonHang>();
        }

        public int MaSp { get; set; }
        public string MaNsx { get; set; } = null!;
        public int? SoLuong { get; set; }
        public string? TenSp { get; set; }
        public string? CauHinh { get; set; }
        public string? MoTa { get; set; }
        public string? PhienBan { get; set; }
        public int? KhuyenMai { get; set; }
        public string? MauSac { get; set; }
        public decimal? Giaban { get; set; }
        public string? AnhSp { get; set; }

        public virtual Nsx MaNsxNavigation { get; set; } = null!;
        public virtual ICollection<DonHang> DonHangs { get; set; }
    }
}
