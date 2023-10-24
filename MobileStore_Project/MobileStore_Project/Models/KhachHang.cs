using System;
using System.Collections.Generic;

namespace MobileStore_Project.Models
{
    public partial class KhachHang
    {
        public KhachHang()
        {
            DonHangs = new HashSet<DonHang>();
        }

        public string Id { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Passw { get; set; } = null!;
        public string HoTen { get; set; } = null!;
        public string? SoDt { get; set; }
        public string? DiaChi { get; set; }

        public virtual ICollection<DonHang> DonHangs { get; set; }
    }
}
