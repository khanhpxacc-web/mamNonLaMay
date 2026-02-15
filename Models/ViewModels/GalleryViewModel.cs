using System.Collections.Generic;
using MamNonApp.Models.Entities;

namespace MamNonApp.Models.ViewModels
{
    /// <summary>
    /// ViewModel cho trang thư viện ảnh
    /// </summary>
    public class GalleryViewModel
    {
        public SeoViewModel Seo { get; set; } = new();
        public IEnumerable<GalleryItem> GalleryItems { get; set; } = new List<GalleryItem>();
        public IEnumerable<string> Categories { get; set; } = new List<string>();
        public string? SelectedCategory { get; set; }
    }
}
