using System;
using System.Collections.Generic;

namespace MobileStore_Project.Models
{
    public partial class Admin
    {
        public string Idadmin { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Passw { get; set; } = null!;
        public string Role { get; set; } = null!;
    }
}
