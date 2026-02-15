using System.ComponentModel.DataAnnotations;

namespace MamNonApp.Models.ViewModels
{
    /// <summary>
    /// ViewModel cho trang liên hệ
    /// </summary>
    public class ContactViewModel
    {
        public SeoViewModel Seo { get; set; } = new();
        public Models.Entities.SchoolInfo? SchoolInfo { get; set; }
        public ContactFormViewModel Form { get; set; } = new();
        public bool IsSuccess { get; set; }
        public string? SuccessMessage { get; set; }
        public string? ErrorMessage { get; set; }
    }

    /// <summary>
    /// Form liên hệ
    /// </summary>
    public class ContactFormViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        [Display(Name = "Họ tên phụ huynh")]
        [StringLength(100)]
        public string ParentName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [Display(Name = "Số điện thoại")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        public string Phone { get; set; } = string.Empty;

        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string? Email { get; set; }

        [Display(Name = "Họ tên bé")]
        [StringLength(100)]
        public string? ChildName { get; set; }

        [Display(Name = "Tuổi của bé")]
        [Range(1, 6, ErrorMessage = "Tuổi phải từ 1 đến 6")]
        public int? ChildAge { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập nội dung")]
        [Display(Name = "Nội dung")]
        [StringLength(1000, ErrorMessage = "Nội dung không quá 1000 ký tự")]
        public string Message { get; set; } = string.Empty;
    }
}
