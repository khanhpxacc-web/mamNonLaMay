using System.ComponentModel.DataAnnotations;

namespace MamNonApp.Models.Entities
{
    /// <summary>
    /// Entity Giáo viên
    /// </summary>
    public class Teacher : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [StringLength(200)]
        public string? Position { get; set; }

        [StringLength(500)]
        public string? Bio { get; set; }

        [StringLength(255)]
        public string? AvatarUrl { get; set; }

        [StringLength(100)]
        public string? Email { get; set; }

        [StringLength(20)]
        public string? Phone { get; set; }

        public int ExperienceYears { get; set; }
        public string? Qualifications { get; set; }
    }
}
