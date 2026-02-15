using System.ComponentModel.DataAnnotations;

namespace MamNonApp.Models.Entities
{
    /// <summary>
    /// Entity Thông tin trường học
    /// </summary>
    public class SchoolInfo : BaseEntity
    {
        [StringLength(200)]
        public string Name { get; set; } = "Trường Mầm Non Hoa Hướng Dương";

        [StringLength(500)]
        public string Address { get; set; } = string.Empty;

        [StringLength(20)]
        public string Phone { get; set; } = string.Empty;

        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [StringLength(255)]
        public string? Website { get; set; }

        [StringLength(200)]
        public string? FacebookUrl { get; set; }

        [StringLength(200)]
        public string? YoutubeUrl { get; set; }

        [StringLength(2000)]
        public string? Description { get; set; }

        [StringLength(2000)]
        public string? Mission { get; set; }

        [StringLength(2000)]
        public string? Vision { get; set; }

        public int EstablishedYear { get; set; }
        public int StudentCount { get; set; }
        public int TeacherCount { get; set; }
        public int ClassCount { get; set; }
    }
}
