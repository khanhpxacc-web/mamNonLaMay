using System;
using System.ComponentModel.DataAnnotations;

namespace MamNonApp.Models.Entities
{
    /// <summary>
    /// Entity Tin tức/Bài viết
    /// </summary>
    public class News : BaseEntity
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [StringLength(200)]
        public string? Slug { get; set; }

        [StringLength(500)]
        public string? Summary { get; set; }

        [StringLength(10000)]
        public string? Content { get; set; }

        [StringLength(255)]
        public string? ImageUrl { get; set; }

        [StringLength(100)]
        public string? Author { get; set; }

        [StringLength(100)]
        public string? Category { get; set; } // Tin tức, Thông báo, Kiến thức, etc.

        public string? Tags { get; set; }
        public bool IsFeatured { get; set; } = false;
        public int ViewCount { get; set; } = 0;
        public DateTime? PublishedAt { get; set; }
    }
}
