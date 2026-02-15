using System;
using System.ComponentModel.DataAnnotations;

namespace MamNonApp.Models.Entities
{
    /// <summary>
    /// Entity Hoạt động của trường
    /// </summary>
    public class Activity : BaseEntity
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Summary { get; set; }

        [StringLength(5000)]
        public string? Content { get; set; }

        [StringLength(255)]
        public string? ImageUrl { get; set; }

        [StringLength(255)]
        public string? ThumbnailUrl { get; set; }

        [StringLength(100)]
        public string? Category { get; set; } // Ngoại khóa, Vui chơi, Học tập, Lễ hội, etc.

        public DateTime? EventDate { get; set; }
        public string? Location { get; set; }
        public bool IsFeatured { get; set; } = false;
        public int ViewCount { get; set; } = 0;
        public int DisplayOrder { get; set; } = 0;
    }
}
