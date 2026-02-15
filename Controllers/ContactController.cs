using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MamNonApp.Interfaces;
using MamNonApp.Models.Entities;
using MamNonApp.Models.ViewModels;

namespace MamNonApp.Controllers
{
    /// <summary>
    /// Contact Controller - Xử lý liên hệ
    /// </summary>
    public class ContactController : Controller
    {
        private readonly ILogger<ContactController> _logger;
        private readonly IContactService _contactService;
        private readonly ISchoolInfoService _schoolInfoService;
        private readonly ISeoService _seoService;

        public ContactController(
            ILogger<ContactController> logger,
            IContactService contactService,
            ISchoolInfoService schoolInfoService,
            ISeoService seoService)
        {
            _logger = logger;
            _contactService = contactService;
            _schoolInfoService = schoolInfoService;
            _seoService = seoService;
        }

        /// <summary>
        /// Trang liên hệ - GET
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = new ContactViewModel
            {
                Seo = _seoService.GetContactSeo(),
                SchoolInfo = await _schoolInfoService.GetSchoolInfoAsync()
            };

            return View(viewModel);
        }

        /// <summary>
        /// Xử lý form liên hệ - POST
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ContactFormViewModel form)
        {
            var viewModel = new ContactViewModel
            {
                Seo = _seoService.GetContactSeo(),
                SchoolInfo = await _schoolInfoService.GetSchoolInfoAsync(),
                Form = form
            };

            if (!ModelState.IsValid)
            {
                viewModel.ErrorMessage = "Vui lòng kiểm tra lại thông tin đã nhập.";
                return View(viewModel);
            }

            // Chuyển đổi từ ViewModel sang Entity
            var message = new ContactMessage
            {
                ParentName = form.ParentName,
                Phone = form.Phone,
                Email = form.Email,
                ChildName = form.ChildName,
                ChildAge = form.ChildAge,
                Message = form.Message
            };

            var result = await _contactService.SendContactMessageAsync(message);

            if (result)
            {
                viewModel.IsSuccess = true;
                viewModel.SuccessMessage = "Cảm ơn bạn đã liên hệ! Chúng tôi sẽ phản hồi trong thời gian sớm nhất.";
                viewModel.Form = new ContactFormViewModel(); // Reset form
                _logger.LogInformation("Nhận được liên hệ từ: {ParentName}", form.ParentName);
            }
            else
            {
                viewModel.ErrorMessage = "Có lỗi xảy ra. Vui lòng thử lại sau.";
                _logger.LogError("Lỗi khi gửi liên hệ từ: {ParentName}", form.ParentName);
            }

            return View(viewModel);
        }
    }
}

