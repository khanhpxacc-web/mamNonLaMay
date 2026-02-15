using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MamNonApp.Interfaces;
using MamNonApp.Models.ViewModels;

namespace MamNonApp.Controllers
{
    /// <summary>
    /// Activities Controller - Xử lý trang hoạt động
    /// </summary>
    public class ActivitiesController : Controller
    {
        private readonly ILogger<ActivitiesController> _logger;
        private readonly IActivityService _activityService;
        private readonly ISeoService _seoService;

        public ActivitiesController(
            ILogger<ActivitiesController> logger,
            IActivityService activityService,
            ISeoService seoService)
        {
            _logger = logger;
            _activityService = activityService;
            _seoService = seoService;
        }

        /// <summary>
        /// Danh sách hoạt động
        /// </summary>
        [Route("hoat-dong")]
        public async Task<IActionResult> Index(string? category = null)
        {
            var activities = string.IsNullOrEmpty(category)
                ? await _activityService.GetActiveActivitiesAsync()
                : await _activityService.GetActivitiesByCategoryAsync(category);

            var viewModel = new ActivitiesViewModel
            {
                Seo = new SeoViewModel
                {
                    Title = string.IsNullOrEmpty(category) 
                        ? "Hoạt Động | Trường Mầm Non Hoa Hướng Dương"
                        : $"Hoạt Động - {category} | Trường Mầm Non Hoa Hướng Dương",
                    MetaDescription = "Khám phá các hoạt động phong phú tại Trường Mầm Non Hoa Hướng Dương: ngoại khóa, lễ hội, thể thao, vui chơi.",
                    MetaKeywords = "hoạt động mầm non, ngoại khóa, lễ hội, thể thao",
                    CanonicalUrl = string.IsNullOrEmpty(category) 
                        ? "https://mamnonlamay.edu.vn/hoat-dong"
                        : $"https://mamnonlamay.edu.vn/hoat-dong?category={category}",
                    OgTitle = "Hoạt Động Tại Hoa Hướng Dương",
                    OgDescription = "Các hoạt động phong phú cho trẻ",
                    OgImage = "https://mamnonlamay.edu.vn/images/activities-og.jpg",
                    OgType = "website"
                },
                Activities = activities,
                Categories = await _activityService.GetCategoriesAsync(),
                SelectedCategory = category
            };

            return View(viewModel);
        }

        /// <summary>
        /// Chi tiết hoạt động
        /// </summary>
        [Route("hoat-dong/{id}/{slug?}")]
        public async Task<IActionResult> Detail(int id, string? slug)
        {
            var activity = await _activityService.GetActivityByIdAsync(id);

            if (activity == null || !activity.IsActive)
            {
                return NotFound();
            }

            // Increment view count
            await _activityService.IncrementViewCountAsync(id);

            var viewModel = new ActivityDetailViewModel
            {
                Seo = new SeoViewModel
                {
                    Title = $"{activity.Title} | Hoạt Động | Trường Mầm Non Hoa Hướng Dương",
                    MetaDescription = activity.Summary ?? $"Chi tiết về {activity.Title}",
                    MetaKeywords = $"{activity.Title}, hoạt động mầm non, {activity.Category}",
                    CanonicalUrl = $"https://mamnonlamay.edu.vn/hoat-dong/{id}/{slug}",
                    OgTitle = activity.Title,
                    OgDescription = activity.Summary ?? activity.Title,
                    OgImage = activity.ImageUrl ?? "/images/og-image.jpg",
                    OgType = "article"
                },
                Activity = activity,
                RelatedActivities = await _activityService.GetFeaturedActivitiesAsync(4)
            };

            return View(viewModel);
        }
    }
}

