using System.Collections.Generic;
using MamNonApp.Models.Entities;

namespace MamNonApp.Models.ViewModels
{
    /// <summary>
    /// ViewModel cho trang giới thiệu
    /// </summary>
    public class AboutViewModel
    {
        public SeoViewModel Seo { get; set; } = new();
        public SchoolInfo? SchoolInfo { get; set; }
        public IEnumerable<Teacher> Teachers { get; set; } = new List<Teacher>();
    }
}
