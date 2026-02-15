using System.ComponentModel.DataAnnotations;

namespace MamNonApp.Models.Entities
{
    /// <summary>
    /// Entity Tin nhắn liên hệ
    /// </summary>
    public class ContactMessage : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string ParentName { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string Phone { get; set; } = string.Empty;

        [StringLength(100)]
        public string? Email { get; set; }

        [StringLength(100)]
        public string? ChildName { get; set; }

        public int? ChildAge { get; set; }

        [Required]
        [StringLength(1000)]
        public string Message { get; set; } = string.Empty;

        public bool IsRead { get; set; } = false;
    }
}
