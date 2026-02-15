using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MamNonApp.Interfaces;
using MamNonApp.Models;
using MamNonApp.Models.ViewModels;

namespace MamNonApp.Controllers
{
    /// <summary>
    /// Home Controller - Xử lý các trang chính
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISchoolInfoService _schoolInfoService;
        private readonly IProgramService _programService;
        private readonly ITeacherService _teacherService;
        private readonly IGalleryService _galleryService;
        private readonly IActivityService _activityService;
        private readonly INewsService _newsService;
        private readonly ISeoService _seoService;

        // Dependency Injection qua constructor
        public HomeController(
            ILogger<HomeController> logger,
            ISchoolInfoService schoolInfoService,
            IProgramService programService,
            ITeacherService teacherService,
            IGalleryService galleryService,
            IActivityService activityService,
            INewsService newsService,
            ISeoService seoService)
        {
            _logger = logger;
            _schoolInfoService = schoolInfoService;
            _programService = programService;
            _teacherService = teacherService;
            _galleryService = galleryService;
            _activityService = activityService;
            _newsService = newsService;
            _seoService = seoService;
        }

        /// <summary>
        /// Trang chủ - Optimized cho SEO
        /// </summary>
        public async Task<IActionResult> Index()
        {
            var viewModel = new HomeViewModel
            {
                Seo = _seoService.GetHomeSeo(),
                SchoolInfo = await _schoolInfoService.GetSchoolInfoAsync(),
                FeaturedPrograms = await _programService.GetActiveProgramsAsync(),
                FeaturedTeachers = await _teacherService.GetFeaturedTeachersAsync(4),
                Testimonials = await _schoolInfoService.GetFeaturedTestimonialsAsync(3),
                FeaturedGallery = await _galleryService.GetFeaturedGalleryItemsAsync(6),
                FeaturedActivities = await _activityService.GetFeaturedActivitiesAsync(3),
                LatestNews = await _newsService.GetLatestNewsAsync(3)
            };

            // Thêm Structured Data cho SEO
            viewModel.Seo.StructuredData = _seoService.GenerateStructuredData("organization", null);

            _logger.LogInformation("Truy cập trang chủ");
            return View(viewModel);
        }

        /// <summary>
        /// Trang giới thiệu
        /// </summary>
        public async Task<IActionResult> About()
        {
            var viewModel = new AboutViewModel
            {
                Seo = _seoService.GetAboutSeo(),
                SchoolInfo = await _schoolInfoService.GetSchoolInfoAsync(),
                Teachers = await _teacherService.GetActiveTeachersAsync()
            };

            _logger.LogInformation("Truy cập trang giới thiệu");
            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

