using System.Collections.Generic;
using MamNonApp.Models.Entities;

namespace MamNonApp.Models.ViewModels
{
    /// <summary>
    /// ViewModel cho trang chủ
    /// </summary>
    public class HomeViewModel
    {
        public SeoViewModel Seo { get; set; } = new();
        public SchoolInfo? SchoolInfo { get; set; }
        public IEnumerable<EducationProgram> FeaturedPrograms { get; set; } = new List<EducationProgram>();
        public IEnumerable<Teacher> FeaturedTeachers { get; set; } = new List<Teacher>();
        public IEnumerable<Testimonial> Testimonials { get; set; } = new List<Testimonial>();
        public IEnumerable<GalleryItem> FeaturedGallery { get; set; } = new List<GalleryItem>();
        public IEnumerable<Activity> FeaturedActivities { get; set; } = new List<Activity>();
        public IEnumerable<News> LatestNews { get; set; } = new List<News>();
    }
}
