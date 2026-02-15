using System.ComponentModel.DataAnnotations;

namespace MamNonApp.Models.Entities
{
    /// <summary>
    /// Entity Đánh giá/Phản hồi từ phụ huynh
    /// </summary>
    public class Testimonial : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string ParentName { get; set; } = string.Empty;

        [StringLength(255)]
        public string? AvatarUrl { get; set; }

        [Required]
        [StringLength(1000)]
        public string Content { get; set; } = string.Empty;

        public int Rating { get; set; } = 5; // 1-5 sao

        [StringLength(100)]
        public string? ChildName { get; set; }

        public bool IsFeatured { get; set; } = false;
        public int DisplayOrder { get; set; }
    }
}
