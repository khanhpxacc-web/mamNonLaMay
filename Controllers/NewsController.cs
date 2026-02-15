using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MamNonApp.Interfaces;
using MamNonApp.Models.ViewModels;

namespace MamNonApp.Controllers
{
    /// <summary>
    /// News Controller - Xử lý trang tin tức/bài viết
    /// </summary>
    public class NewsController : Controller
    {
        private readonly ILogger<NewsController> _logger;
        private readonly INewsService _newsService;
        private readonly ISeoService _seoService;

        public NewsController(
            ILogger<NewsController> logger,
            INewsService newsService,
            ISeoService seoService)
        {
            _logger = logger;
            _newsService = newsService;
            _seoService = seoService;
        }

        /// <summary>
        /// Danh sách tin tức
        /// </summary>
        [Route("tin-tuc")]
        public async Task<IActionResult> Index(string? category = null, string? keyword = null)
        {
            var news = string.IsNullOrEmpty(category)
                ? await _newsService.GetPublishedNewsAsync()
                : await _newsService.GetNewsByCategoryAsync(category);

            // Filter by keyword if provided
            if (!string.IsNullOrEmpty(keyword))
            {
                news = await _newsService.SearchNewsAsync(keyword);
            }

            var viewModel = new NewsViewModel
            {
                Seo = new SeoViewModel
                {
                    Title = string.IsNullOrEmpty(category) 
                        ? "Tin Tức & Bài Viết | Trường Mầm Non Hoa Hướng Dương"
                        : $"Tin Tức - {category} | Trường Mầm Non Hoa Hướng Dương",
                    MetaDescription = "Cập nhật tin tức, thông báo, kiến thức giáo dục mầm non từ Trường Hoa Hướng Dương.",
                    MetaKeywords = "tin tức mầm non, thông báo, kiến thức giáo dục",
                    CanonicalUrl = string.IsNullOrEmpty(category) 
                        ? "https://mamnonlamay.edu.vn/tin-tuc"
                        : $"https://mamnonlamay.edu.vn/tin-tuc?category={category}",
                    OgTitle = "Tin Tức & Bài Viết",
                    OgDescription = "Thông tin mới nhất từ trường",
                    OgImage = "https://mamnonlamay.edu.vn/images/news-og.jpg",
                    OgType = "website"
                },
                News = news,
                Categories = await _newsService.GetCategoriesAsync(),
                SelectedCategory = category,
                Keyword = keyword
            };

            return View(viewModel);
        }

        /// <summary>
        /// Chi tiết tin tức theo ID
        /// </summary>
        [Route("tin-tuc/{id}/{slug?}")]
        public async Task<IActionResult> Detail(int id, string? slug)
        {
            var newsItem = await _newsService.GetNewsByIdAsync(id);

            if (newsItem == null || !newsItem.IsActive)
            {
                return NotFound();
            }

            // Increment view count
            await _newsService.IncrementViewCountAsync(id);

            var allNews = await _newsService.GetPublishedNewsAsync();

            var viewModel = new NewsDetailViewModel
            {
                Seo = new SeoViewModel
                {
                    Title = $"{newsItem.Title} | Tin Tức | Trường Mầm Non Hoa Hướng Dương",
                    MetaDescription = newsItem.Summary ?? $"Chi tiết về {newsItem.Title}",
                    MetaKeywords = $"{newsItem.Title}, {newsItem.Tags}, tin tức mầm non",
                    CanonicalUrl = $"https://mamnonlamay.edu.vn/tin-tuc/{id}/{newsItem.Slug}",
                    OgTitle = newsItem.Title,
                    OgDescription = newsItem.Summary ?? newsItem.Title,
                    OgImage = newsItem.ImageUrl ?? "/images/og-image.jpg",
                    OgType = "article"
                },
                NewsItem = newsItem,
                RelatedNews = await _newsService.GetLatestNewsAsync(4)
            };

            return View(viewModel);
        }
    }
}

