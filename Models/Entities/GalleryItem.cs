using System.ComponentModel.DataAnnotations;

namespace MamNonApp.Models.Entities
{
    /// <summary>
    /// Entity Ảnh/Video trong thư viện
    /// </summary>
    public class GalleryItem : BaseEntity
    {
        [Required]
        [StringLength(255)]
        public string ImageUrl { get; set; } = string.Empty;

        [StringLength(200)]
        public string? Title { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [StringLength(50)]
        public string? Category { get; set; } // Hoạt động, Lớp học, Sự kiện, etc.

        public bool IsFeatured { get; set; }
        public int DisplayOrder { get; set; }
    }
}
