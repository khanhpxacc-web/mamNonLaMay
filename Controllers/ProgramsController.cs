using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MamNonApp.Interfaces;
using MamNonApp.Models.ViewModels;

namespace MamNonApp.Controllers
{
    /// <summary>
    /// Programs Controller - Xử lý chương trình học
    /// </summary>
    public class ProgramsController : Controller
    {
        private readonly ILogger<ProgramsController> _logger;
        private readonly IProgramService _programService;
        private readonly ISeoService _seoService;

        public ProgramsController(
            ILogger<ProgramsController> logger,
            IProgramService programService,
            ISeoService seoService)
        {
            _logger = logger;
            _programService = programService;
            _seoService = seoService;
        }

        /// <summary>
        /// Danh sách chương trình học
        /// </summary>
        public async Task<IActionResult> Index()
        {
            var viewModel = new ProgramsViewModel
            {
                Seo = _seoService.GetProgramsSeo(),
                Programs = await _programService.GetActiveProgramsAsync()
            };

            return View(viewModel);
        }

        /// <summary>
        /// Chi tiết chương trình học - SEO friendly URL
        /// </summary>
        [Route("chuong-trinh-hoc/{id}/{slug?}")]
        public async Task<IActionResult> Detail(int id, string? slug)
        {
            var program = await _programService.GetProgramByIdAsync(id);

            if (program == null || !program.IsActive)
            {
                return NotFound();
            }

            var viewModel = new ProgramDetailViewModel
            {
                Seo = new SeoViewModel
                {
                    Title = $"{program.Title} | Trường Mầm Non Hoa Hướng Dương",
                    MetaDescription = program.Description ?? $"Chi tiết về {program.Title}",
                    MetaKeywords = $"{program.Title}, chương trình mầm non, {program.AgeGroup}",
                    CanonicalUrl = $"https://mamnonlamay.edu.vn/chuong-trinh-hoc/{id}/{slug}",
                    OgTitle = program.Title,
                    OgDescription = program.Description ?? program.Title,
                    OgImage = program.ImageUrl ?? "/images/og-image.jpg",
                    OgType = "article"
                },
                Program = program
            };

            return View(viewModel);
        }
    }
}

