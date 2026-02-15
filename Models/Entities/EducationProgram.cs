using System.ComponentModel.DataAnnotations;

namespace MamNonApp.Models.Entities
{
    /// <summary>
    /// Entity Chương trình học
    /// </summary>
    public class EducationProgram : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        [StringLength(2000)]
        public string? Content { get; set; }

        [StringLength(255)]
        public string? ImageUrl { get; set; }

        [StringLength(50)]
        public string? AgeGroup { get; set; } // 1-2 tuổi, 3-5 tuổi, etc.

        public decimal? Price { get; set; }
        public int Duration { get; set; } // Số buổi/tuần
        public string? Schedule { get; set; }
        public int DisplayOrder { get; set; }
    }
}
