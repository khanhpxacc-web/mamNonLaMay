using System;

namespace MamNonApp.Models.Entities
{
    /// <summary>
    /// Base entity cho tất cả các entities trong hệ thống
    /// </summary>
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
