using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MamNonApp.Interfaces;
using MamNonApp.Models.ViewModels;

namespace MamNonApp.Controllers
{
    /// <summary>
    /// Gallery Controller - Xử lý thư viện ảnh
    /// </summary>
    public class GalleryController : Controller
    {
        private readonly ILogger<GalleryController> _logger;
        private readonly IGalleryService _galleryService;
        private readonly ISeoService _seoService;

        public GalleryController(
            ILogger<GalleryController> logger,
            IGalleryService galleryService,
            ISeoService seoService)
        {
            _logger = logger;
            _galleryService = galleryService;
            _seoService = seoService;
        }

        /// <summary>
        /// Thư viện ảnh với filter theo category
        /// </summary>
        public async Task<IActionResult> Index(string? category = null)
        {
            var galleryItems = string.IsNullOrEmpty(category)
                ? await _galleryService.GetAllGalleryItemsAsync()
                : await _galleryService.GetGalleryItemsByCategoryAsync(category);

            var viewModel = new GalleryViewModel
            {
                Seo = _seoService.GetGallerySeo(),
                GalleryItems = galleryItems,
                Categories = await _galleryService.GetCategoriesAsync(),
                SelectedCategory = category
            };

            return View(viewModel);
        }
    }
}

