using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MobileStore_Project.Models
{
    public partial class KhachHang
    {
        public KhachHang()
        {
            DonHangs = new HashSet<DonHang>();
        }

        public string Id { get; set; } = null!;
        [Required(ErrorMessage = "Vui lòng nhập trường này")]
        [RegularExpression(@"^[A-Za-z0-9._%+-]+@gmail\.com$", ErrorMessage = "Vui lòng nhập chính xác dữ liệu, email cần có gmail.com")]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "Vui lòng nhập trường này")]
        public string Passw { get; set; } = null!;
        [Required(ErrorMessage = "Vui lòng nhập trường này")]
        public string HoTen { get; set; } = null!;
        [Required(ErrorMessage = "Vui lòng nhập trường này")]
        public string? SoDt { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập trường này")]
        public string? DiaChi { get; set; }

        public virtual ICollection<DonHang> DonHangs { get; set; }
    }
}
