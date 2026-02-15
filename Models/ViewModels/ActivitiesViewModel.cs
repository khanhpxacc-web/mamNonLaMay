using System.Collections.Generic;
using MamNonApp.Models.Entities;

namespace MamNonApp.Models.ViewModels
{
    /// <summary>
    /// ViewModel cho trang hoạt động
    /// </summary>
    public class ActivitiesViewModel
    {
        public SeoViewModel Seo { get; set; } = new();
        public IEnumerable<Activity> Activities { get; set; } = new List<Activity>();
        public IEnumerable<string> Categories { get; set; } = new List<string>();
        public string? SelectedCategory { get; set; }
    }

    /// <summary>
    /// ViewModel cho chi tiết hoạt động
    /// </summary>
    public class ActivityDetailViewModel
    {
        public SeoViewModel Seo { get; set; } = new();
        public Activity? Activity { get; set; }
        public IEnumerable<Activity> RelatedActivities { get; set; } = new List<Activity>();
    }
}
