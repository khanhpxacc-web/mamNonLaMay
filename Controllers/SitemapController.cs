using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MamNonApp.Interfaces;

namespace MamNonApp.Controllers
{
    /// <summary>
    /// Sitemap Controller - SEO: Tạo sitemap.xml động
    /// </summary>
    public class SitemapController : Controller
    {
        private readonly IProgramService _programService;
        private readonly IGalleryService _galleryService;
        private readonly IActivityService _activityService;
        private readonly INewsService _newsService;

        public SitemapController(
            IProgramService programService,
            IGalleryService galleryService,
            IActivityService activityService,
            INewsService newsService)
        {
            _programService = programService;
            _galleryService = galleryService;
            _activityService = activityService;
            _newsService = newsService;
        }

        /// <summary>
        /// Generate sitemap.xml động cho SEO
        /// </summary>
        [Route("sitemap.xml")]
        [Produces("application/xml")]
        public async Task<IActionResult> Index()
        {
            var baseUrl = "https://mamnonlamay.edu.vn";
            var sb = new StringBuilder();

            sb.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            sb.AppendLine("<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">");

            // Static pages
            AddUrl(sb, baseUrl, DateTime.Now, "daily", "1.0");
            AddUrl(sb, $"{baseUrl}/gioi-thieu", DateTime.Now, "weekly", "0.8");
            AddUrl(sb, $"{baseUrl}/chuong-trinh-hoc", DateTime.Now, "weekly", "0.9");
            AddUrl(sb, $"{baseUrl}/thu-vien-anh", DateTime.Now, "weekly", "0.7");
            AddUrl(sb, $"{baseUrl}/lien-he", DateTime.Now, "monthly", "0.6");
            AddUrl(sb, $"{baseUrl}/hoat-dong", DateTime.Now, "weekly", "0.8");
            AddUrl(sb, $"{baseUrl}/tin-tuc", DateTime.Now, "daily", "0.9");

            // Dynamic pages - Programs
            var programs = await _programService.GetActiveProgramsAsync();
            foreach (var program in programs)
            {
                var slug = GenerateSlug(program.Title);
                AddUrl(sb, $"{baseUrl}/chuong-trinh-hoc/{program.Id}/{slug}", program.UpdatedAt ?? program.CreatedAt, "monthly", "0.8");
            }

            // Dynamic pages - Activities
            var activities = await _activityService.GetActiveActivitiesAsync();
            foreach (var activity in activities)
            {
                var slug = GenerateSlug(activity.Title);
                AddUrl(sb, $"{baseUrl}/hoat-dong/{activity.Id}/{slug}", activity.UpdatedAt ?? activity.CreatedAt, "weekly", "0.7");
            }

            // Dynamic pages - News
            var news = await _newsService.GetPublishedNewsAsync();
            foreach (var item in news)
            {
                if (!string.IsNullOrEmpty(item.Slug))
                {
                    AddUrl(sb, $"{baseUrl}/tin-tuc/{item.Id}/{item.Slug}", item.UpdatedAt ?? item.CreatedAt, "weekly", "0.8");
                }
            }

            // Dynamic pages - Gallery
            var galleryItems = await _galleryService.GetAllGalleryItemsAsync();
            foreach (var item in galleryItems)
            {
                if (item.IsActive)
                {
                    AddUrl(sb, $"{baseUrl}/thu-vien-anh#{item.Id}", item.UpdatedAt ?? item.CreatedAt, "weekly", "0.6");
                }
            }

            sb.AppendLine("</urlset>");

            return Content(sb.ToString(), "application/xml");
        }

        private void AddUrl(StringBuilder sb, string loc, DateTime lastmod, string changefreq, string priority)
        {
            sb.AppendLine("  <url>");
            sb.AppendLine($"    <loc>{loc}</loc>");
            sb.AppendLine($"    <lastmod>{lastmod:yyyy-MM-dd}</lastmod>");
            sb.AppendLine($"    <changefreq>{changefreq}</changefreq>");
            sb.AppendLine($"    <priority>{priority}</priority>");
            sb.AppendLine("  </url>");
        }

        private string GenerateSlug(string? title)
        {
            if (string.IsNullOrEmpty(title))
                return "no-title";

            // Simple slug generation
            var slug = title.ToLower()
                .Replace(" ", "-")
                .Replace(",", "")
                .Replace(".", "")
                .Replace("(", "")
                .Replace(")", "")
                .Replace("--", "-");

            // Remove Vietnamese accents (simplified)
            slug = slug.Replace("áàảãạăắằẳẵặâấầẩẫậ", "a")
                .Replace("đ", "d")
                .Replace("éèẻẽẹêếềểễệ", "e")
                .Replace("íìỉĩị", "i")
                .Replace("óòỏõọôốồổỗộơớờởỡợ", "o")
                .Replace("úùủũụưứừửữự", "u")
                .Replace("ýỳỷỹỵ", "y");

            return slug;
        }
    }
}

