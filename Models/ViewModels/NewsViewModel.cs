using System.Collections.Generic;
using MamNonApp.Models.Entities;

namespace MamNonApp.Models.ViewModels
{
    /// <summary>
    /// ViewModel cho trang tin tức
    /// </summary>
    public class NewsViewModel
    {
        public SeoViewModel Seo { get; set; } = new();
        public IEnumerable<News> News { get; set; } = new List<News>();
        public IEnumerable<string> Categories { get; set; } = new List<string>();
        public string? SelectedCategory { get; set; }
        public string? Keyword { get; set; }
    }

    /// <summary>
    /// ViewModel cho chi tiết tin tức
    /// </summary>
    public class NewsDetailViewModel
    {
        public SeoViewModel Seo { get; set; } = new();
        public News? NewsItem { get; set; }
        public IEnumerable<News> RelatedNews { get; set; } = new List<News>();
        public News? PreviousNews { get; set; }
        public News? NextNews { get; set; }
    }
}
