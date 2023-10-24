using System;
using System.Collections.Generic;

namespace MobileStore_Project.Models
{
    public partial class Nsx
    {
        public Nsx()
        {
            SanPhams = new HashSet<SanPham>();
        }

        public string MaNsx { get; set; } = null!;
        public string? TenNsx { get; set; }

        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
