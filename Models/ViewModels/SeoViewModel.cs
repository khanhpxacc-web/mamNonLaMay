namespace MamNonApp.Models.ViewModels
{
    /// <summary>
    /// ViewModel cho SEO metadata
    /// </summary>
    public class SeoViewModel
    {
        public string Title { get; set; } = string.Empty;
        public string MetaDescription { get; set; } = string.Empty;
        public string MetaKeywords { get; set; } = string.Empty;
        public string CanonicalUrl { get; set; } = string.Empty;
        public string OgTitle { get; set; } = string.Empty;
        public string OgDescription { get; set; } = string.Empty;
        public string OgImage { get; set; } = string.Empty;
        public string OgType { get; set; } = "website";
        public string? StructuredData { get; set; }
    }
}
