using MamNonApp.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace MamNonApp.Models
{
    // Login View Model
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập email")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Ghi nhớ đăng nhập")]
        public bool RememberMe { get; set; }
    }

    // Admin Dashboard View Model
    public class AdminDashboardViewModel
    {
        public int TotalPrograms { get; set; }
        public int TotalTeachers { get; set; }
        public int TotalActivities { get; set; }
        public int TotalNews { get; set; }
        public int TotalGalleryItems { get; set; }
        public int TotalMessages { get; set; }
        public int PendingMessages { get; set; }
        public List<ContactMessage> RecentMessages { get; set; } = new();
    }
}
