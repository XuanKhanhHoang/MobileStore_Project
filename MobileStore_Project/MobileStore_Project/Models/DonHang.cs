using System;
using System.Collections.Generic;

namespace MobileStore_Project.Models
{
    public partial class DonHang
    {
        public string MaDh { get; set; } = null!;
        public int MaSp { get; set; }
        public string Id { get; set; } = null!;
        public DateTime? NgayTao { get; set; }
        public int? SoLuong { get; set; }
        public string? ThanhToan { get; set; }
        public string? VanChuyen { get; set; }
        public decimal? GiaTri { get; set; }
        public string? TinhTrangDh { get; set; }

        public virtual KhachHang IdNavigation { get; set; } = null!;
        public virtual SanPham MaSpNavigation { get; set; } = null!;
    }
}
