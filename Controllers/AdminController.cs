using MamNonApp.Data;
using MamNonApp.Interfaces;
using MamNonApp.Models;
using MamNonApp.Models.Entities;
using MamNonApp.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MamNonApp.Controllers
{
    [Route("admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<AdminController> _logger;
        private readonly IFileStorageService _fileStorage;

        public AdminController(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<AdminController> logger,
            IFileStorageService fileStorage)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _fileStorage = fileStorage;
        }

        #region Authentication

        [HttpGet("login")]
        [AllowAnonymous]
        public IActionResult Login(string? returnUrl = null)
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost("login")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(
                model.Email,
                model.Password,
                model.RememberMe,
                lockoutOnFailure: true);

            if (result.Succeeded)
            {
                _logger.LogInformation("User logged in.");
                return RedirectToLocal(returnUrl);
            }

            if (result.IsLockedOut)
            {
                _logger.LogWarning("User account locked out.");
                return View("Lockout");
            }

            ModelState.AddModelError(string.Empty, "Đăng nhập không thành công. Vui lòng kiểm tra email và mật khẩu.");
            return View(model);
        }

        [HttpPost("logout")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return RedirectToAction(nameof(Login));
        }

        [HttpGet("access-denied")]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        #endregion

        #region Dashboard

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var dashboard = new AdminDashboardViewModel
            {
                TotalPrograms = await _context.Programs.CountAsync(),
                TotalTeachers = await _context.Teachers.CountAsync(),
                TotalActivities = await _context.Activities.CountAsync(),
                TotalNews = await _context.News.CountAsync(),
                TotalGalleryItems = await _context.GalleryItems.CountAsync(),
                TotalMessages = await _context.ContactMessages.CountAsync(),
                PendingMessages = await _context.ContactMessages.CountAsync(m => !m.IsRead),
                RecentMessages = await _context.ContactMessages
                    .OrderByDescending(m => m.CreatedAt)
                    .Take(5)
                    .ToListAsync()
            };

            return View(dashboard);
        }

        #endregion

        #region Programs Management

        [HttpGet("programs")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Programs(string? search, string? ageGroup, string? sortOrder, int page = 1)
        {
            const int pageSize = 10;
            var query = _context.Programs.AsQueryable();
            
            // Tìm kiếm theo tên
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.Title.Contains(search) || 
                                        (p.Description != null && p.Description.Contains(search)));
            }
            
            // Lọc theo độ tuổi
            if (!string.IsNullOrEmpty(ageGroup))
            {
                query = query.Where(p => p.AgeGroup == ageGroup);
            }
            
            // Sắp xếp
            query = sortOrder switch
            {
                "name_asc" => query.OrderBy(p => p.Title),
                "name_desc" => query.OrderByDescending(p => p.Title),
                "price_asc" => query.OrderBy(p => p.Price),
                "price_desc" => query.OrderByDescending(p => p.Price),
                _ => query.OrderByDescending(p => p.CreatedAt) // Mặc định: mới nhất lên đầu
            };
            
            // Lưu lại các giá trị filter để hiển thị lại form
            ViewBag.Search = search;
            ViewBag.AgeGroup = ageGroup;
            ViewBag.SortOrder = sortOrder;
            
            // Lấy danh sách độ tuổi cho dropdown
            ViewBag.AgeGroups = await _context.Programs
                .Where(p => !string.IsNullOrEmpty(p.AgeGroup))
                .Select(p => p.AgeGroup)
                .Distinct()
                .ToListAsync();
            
            var programs = await PaginatedList<EducationProgram>.CreateAsync(query, page, pageSize);
            return View(programs);
        }

        [HttpGet("programs/create")]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateProgram()
        {
            return View();
        }

        [HttpPost("programs/create")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProgram(EducationProgram program, IFormFile? ImageUrl_file)
        {
            if (ModelState.IsValid)
            {
                // Xử lý upload ảnh nếu có
                if (ImageUrl_file != null && ImageUrl_file.Length > 0)
                {
                    try
                    {
                        var imagePath = await _fileStorage.UploadFileAsync(ImageUrl_file, "programs");
                        if (!string.IsNullOrEmpty(imagePath))
                        {
                            program.ImageUrl = imagePath;
                        }
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", $"Lỗi upload ảnh: {ex.Message}");
                        return View(program);
                    }
                }
                
                _context.Programs.Add(program);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Chương trình học đã được tạo thành công!";
                return RedirectToAction(nameof(Programs));
            }
            return View(program);
        }

        [HttpGet("programs/edit/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditProgram(int id)
        {
            var program = await _context.Programs.FindAsync(id);
            if (program == null)
            {
                return NotFound();
            }
            return View(program);
        }

        [HttpPost("programs/edit/{id}")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProgram(int id, EducationProgram program, IFormFile? ImageUrl_file)
        {
            if (id != program.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Xử lý upload ảnh nếu có
                    if (ImageUrl_file != null && ImageUrl_file.Length > 0)
                    {
                        try
                        {
                            // Xóa ảnh cũ nếu có
                            var existingProgram = await _context.Programs.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
                            if (existingProgram != null && !string.IsNullOrEmpty(existingProgram.ImageUrl) && existingProgram.ImageUrl.StartsWith("/uploads/"))
                            {
                                _fileStorage.DeleteFile(existingProgram.ImageUrl);
                            }
                            
                            var imagePath = await _fileStorage.UploadFileAsync(ImageUrl_file, "programs");
                            if (!string.IsNullOrEmpty(imagePath))
                            {
                                program.ImageUrl = imagePath;
                            }
                        }
                        catch (Exception ex)
                        {
                            ModelState.AddModelError("", $"Lỗi upload ảnh: {ex.Message}");
                            return View(program);
                        }
                    }
                    
                    _context.Update(program);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Chương trình học đã được cập nhật!";
                    return RedirectToAction(nameof(Programs));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _context.Programs.AnyAsync(p => p.Id == id))
                    {
                        return NotFound();
                    }
                    throw;
                }
            }
            return View(program);
        }

        [HttpPost("programs/delete/{id}")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProgram(int id)
        {
            var program = await _context.Programs.FindAsync(id);
            if (program != null)
            {
                // Xóa file ảnh nếu là file upload
                if (!string.IsNullOrEmpty(program.ImageUrl) && program.ImageUrl.StartsWith("/uploads/"))
                {
                    _fileStorage.DeleteFile(program.ImageUrl);
                }
                
                _context.Programs.Remove(program);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Chương trình học đã được xóa!";
            }
            return RedirectToAction(nameof(Programs));
        }

        #endregion

        #region Activities Management

        [HttpGet("activities")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Activities(string? search, string? category, DateTime? fromDate, DateTime? toDate, int page = 1)
        {
            const int pageSize = 10;
            var query = _context.Activities.AsQueryable();
            
            // Tìm kiếm theo tên
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(a => a.Title.Contains(search) || 
                                        (a.Summary != null && a.Summary.Contains(search)));
            }
            
            // Lọc theo danh mục
            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(a => a.Category == category);
            }
            
            // Lọc theo ngày
            if (fromDate.HasValue)
            {
                query = query.Where(a => a.EventDate >= fromDate.Value);
            }
            if (toDate.HasValue)
            {
                query = query.Where(a => a.EventDate <= toDate.Value);
            }
            
            // Mặc định sắp xếp mới nhất lên đầu
            query = query.OrderByDescending(a => a.CreatedAt);
            
            ViewBag.Search = search;
            ViewBag.Category = category;
            ViewBag.FromDate = fromDate?.ToString("yyyy-MM-dd");
            ViewBag.ToDate = toDate?.ToString("yyyy-MM-dd");
            
            // Lấy danh sách danh mục cho dropdown
            ViewBag.Categories = await _context.Activities
                .Where(a => !string.IsNullOrEmpty(a.Category))
                .Select(a => a.Category)
                .Distinct()
                .ToListAsync();
            
            var activities = await PaginatedList<Activity>.CreateAsync(query, page, pageSize);
            return View(activities);
        }

        [HttpGet("activities/create")]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateActivity()
        {
            return View();
        }

        [HttpPost("activities/create")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateActivity(Activity activity, IFormFile? ImageUrl_file, IFormFile? ThumbnailUrl_file)
        {
            if (ModelState.IsValid)
            {
                // Xử lý upload ảnh chính
                if (ImageUrl_file != null && ImageUrl_file.Length > 0)
                {
                    try
                    {
                        var imagePath = await _fileStorage.UploadFileAsync(ImageUrl_file, "activities");
                        if (!string.IsNullOrEmpty(imagePath))
                        {
                            activity.ImageUrl = imagePath;
                        }
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", $"Lỗi upload ảnh chính: {ex.Message}");
                        return View(activity);
                    }
                }
                
                // Xử lý upload thumbnail
                if (ThumbnailUrl_file != null && ThumbnailUrl_file.Length > 0)
                {
                    try
                    {
                        var thumbPath = await _fileStorage.UploadFileAsync(ThumbnailUrl_file, "activities/thumbs");
                        if (!string.IsNullOrEmpty(thumbPath))
                        {
                            activity.ThumbnailUrl = thumbPath;
                        }
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", $"Lỗi upload thumbnail: {ex.Message}");
                        return View(activity);
                    }
                }
                
                _context.Activities.Add(activity);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Hoạt động đã được tạo thành công!";
                return RedirectToAction(nameof(Activities));
            }
            return View(activity);
        }

        [HttpGet("activities/edit/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditActivity(int id)
        {
            var activity = await _context.Activities.FindAsync(id);
            if (activity == null)
            {
                return NotFound();
            }
            return View(activity);
        }

        [HttpPost("activities/edit/{id}")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditActivity(int id, Activity activity, IFormFile? ImageUrl_file, IFormFile? ThumbnailUrl_file)
        {
            if (id != activity.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                // Xử lý upload ảnh chính
                if (ImageUrl_file != null && ImageUrl_file.Length > 0)
                {
                    try
                    {
                        var existing = await _context.Activities.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
                        if (existing != null && !string.IsNullOrEmpty(existing.ImageUrl) && existing.ImageUrl.StartsWith("/uploads/"))
                        {
                            _fileStorage.DeleteFile(existing.ImageUrl);
                        }
                        
                        var imagePath = await _fileStorage.UploadFileAsync(ImageUrl_file, "activities");
                        if (!string.IsNullOrEmpty(imagePath))
                        {
                            activity.ImageUrl = imagePath;
                        }
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", $"Lỗi upload ảnh chính: {ex.Message}");
                        return View(activity);
                    }
                }
                
                // Xử lý upload thumbnail
                if (ThumbnailUrl_file != null && ThumbnailUrl_file.Length > 0)
                {
                    try
                    {
                        var existing = await _context.Activities.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
                        if (existing != null && !string.IsNullOrEmpty(existing.ThumbnailUrl) && existing.ThumbnailUrl.StartsWith("/uploads/"))
                        {
                            _fileStorage.DeleteFile(existing.ThumbnailUrl);
                        }
                        
                        var thumbPath = await _fileStorage.UploadFileAsync(ThumbnailUrl_file, "activities/thumbs");
                        if (!string.IsNullOrEmpty(thumbPath))
                        {
                            activity.ThumbnailUrl = thumbPath;
                        }
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", $"Lỗi upload thumbnail: {ex.Message}");
                        return View(activity);
                    }
                }
                
                _context.Update(activity);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Hoạt động đã được cập nhật!";
                return RedirectToAction(nameof(Activities));
            }
            return View(activity);
        }

        [HttpPost("activities/delete/{id}")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteActivity(int id)
        {
            var activity = await _context.Activities.FindAsync(id);
            if (activity != null)
            {
                // Xóa file ảnh nếu là file upload
                if (!string.IsNullOrEmpty(activity.ImageUrl) && activity.ImageUrl.StartsWith("/uploads/"))
                {
                    _fileStorage.DeleteFile(activity.ImageUrl);
                }
                if (!string.IsNullOrEmpty(activity.ThumbnailUrl) && activity.ThumbnailUrl.StartsWith("/uploads/"))
                {
                    _fileStorage.DeleteFile(activity.ThumbnailUrl);
                }
                
                _context.Activities.Remove(activity);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Hoạt động đã được xóa!";
            }
            return RedirectToAction(nameof(Activities));
        }

        #endregion

        #region News Management

        [HttpGet("news")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> News(string? search, string? category, string? author, int page = 1)
        {
            const int pageSize = 10;
            var query = _context.News.AsQueryable();
            
            // Tìm kiếm theo tên
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(n => n.Title.Contains(search) || 
                                        (n.Summary != null && n.Summary.Contains(search)));
            }
            
            // Lọc theo danh mục
            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(n => n.Category == category);
            }
            
            // Lọc theo tác giả
            if (!string.IsNullOrEmpty(author))
            {
                query = query.Where(n => n.Author == author);
            }
            
            // Mặc định sắp xếp mới nhất lên đầu
            query = query.OrderByDescending(n => n.CreatedAt);
            
            ViewBag.Search = search;
            ViewBag.Category = category;
            ViewBag.Author = author;
            
            // Lấy danh sách cho dropdown
            ViewBag.Categories = await _context.News
                .Where(n => !string.IsNullOrEmpty(n.Category))
                .Select(n => n.Category)
                .Distinct()
                .ToListAsync();
                
            ViewBag.Authors = await _context.News
                .Where(n => !string.IsNullOrEmpty(n.Author))
                .Select(n => n.Author)
                .Distinct()
                .ToListAsync();
            
            var news = await PaginatedList<News>.CreateAsync(query, page, pageSize);
            return View(news);
        }

        [HttpGet("news/create")]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateNews()
        {
            return View();
        }

        [HttpPost("news/create")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateNews(News news, IFormFile? ImageUrl_file)
        {
            if (ModelState.IsValid)
            {
                // Xử lý upload ảnh
                if (ImageUrl_file != null && ImageUrl_file.Length > 0)
                {
                    try
                    {
                        var imagePath = await _fileStorage.UploadFileAsync(ImageUrl_file, "news");
                        if (!string.IsNullOrEmpty(imagePath))
                        {
                            news.ImageUrl = imagePath;
                        }
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", $"Lỗi upload ảnh: {ex.Message}");
                        return View(news);
                    }
                }
                
                _context.News.Add(news);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Tin tức đã được tạo thành công!";
                return RedirectToAction(nameof(News));
            }
            return View(news);
        }

        [HttpGet("news/edit/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditNews(int id)
        {
            var news = await _context.News.FindAsync(id);
            if (news == null)
            {
                return NotFound();
            }
            return View(news);
        }

        [HttpPost("news/edit/{id}")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditNews(int id, News news, IFormFile? ImageUrl_file)
        {
            if (id != news.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                // Xử lý upload ảnh
                if (ImageUrl_file != null && ImageUrl_file.Length > 0)
                {
                    try
                    {
                        var existing = await _context.News.AsNoTracking().FirstOrDefaultAsync(n => n.Id == id);
                        if (existing != null && !string.IsNullOrEmpty(existing.ImageUrl) && existing.ImageUrl.StartsWith("/uploads/"))
                        {
                            _fileStorage.DeleteFile(existing.ImageUrl);
                        }
                        
                        var imagePath = await _fileStorage.UploadFileAsync(ImageUrl_file, "news");
                        if (!string.IsNullOrEmpty(imagePath))
                        {
                            news.ImageUrl = imagePath;
                        }
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", $"Lỗi upload ảnh: {ex.Message}");
                        return View(news);
                    }
                }
                
                _context.Update(news);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Tin tức đã được cập nhật!";
                return RedirectToAction(nameof(News));
            }
            return View(news);
        }

        [HttpPost("news/delete/{id}")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteNews(int id)
        {
            var news = await _context.News.FindAsync(id);
            if (news != null)
            {
                // Xóa file ảnh nếu là file upload
                if (!string.IsNullOrEmpty(news.ImageUrl) && news.ImageUrl.StartsWith("/uploads/"))
                {
                    _fileStorage.DeleteFile(news.ImageUrl);
                }
                
                _context.News.Remove(news);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Tin tức đã được xóa!";
            }
            return RedirectToAction(nameof(News));
        }

        #endregion

        #region Gallery Management

        [HttpGet("gallery")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Gallery(string? search, string? category, int page = 1)
        {
            const int pageSize = 12; // Gallery hiển thị 12 ảnh/trang
            var query = _context.GalleryItems.AsQueryable();
            
            // Tìm kiếm theo tên
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(g => g.Title.Contains(search) || 
                                        (g.Description != null && g.Description.Contains(search)));
            }
            
            // Lọc theo danh mục
            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(g => g.Category == category);
            }
            
            // Mặc định sắp xếp mới nhất lên đầu
            query = query.OrderByDescending(g => g.CreatedAt);
            
            ViewBag.Search = search;
            ViewBag.Category = category;
            
            // Lấy danh sách danh mục cho dropdown
            ViewBag.Categories = await _context.GalleryItems
                .Where(g => !string.IsNullOrEmpty(g.Category))
                .Select(g => g.Category)
                .Distinct()
                .ToListAsync();
            
            var items = await PaginatedList<GalleryItem>.CreateAsync(query, page, pageSize);
            return View(items);
        }

        [HttpGet("gallery/create")]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateGallery()
        {
            return View();
        }

        [HttpPost("gallery/create")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateGallery(GalleryItem item, IFormFile? ImageUrl_file)
        {
            if (ModelState.IsValid)
            {
                // Xử lý upload ảnh (bắt buộc cho gallery)
                if (ImageUrl_file != null && ImageUrl_file.Length > 0)
                {
                    try
                    {
                        var imagePath = await _fileStorage.UploadFileAsync(ImageUrl_file, "gallery");
                        if (!string.IsNullOrEmpty(imagePath))
                        {
                            item.ImageUrl = imagePath;
                        }
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", $"Lỗi upload ảnh: {ex.Message}");
                        return View(item);
                    }
                }
                else if (string.IsNullOrEmpty(item.ImageUrl))
                {
                    ModelState.AddModelError("", "Vui lòng chọn ảnh hoặc nhập URL");
                    return View(item);
                }
                
                _context.GalleryItems.Add(item);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Ảnh đã được thêm vào thư viện!";
                return RedirectToAction(nameof(Gallery));
            }
            return View(item);
        }

        [HttpPost("gallery/delete/{id}")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteGallery(int id)
        {
            var item = await _context.GalleryItems.FindAsync(id);
            if (item != null)
            {
                // Xóa file ảnh nếu là file upload
                if (!string.IsNullOrEmpty(item.ImageUrl) && item.ImageUrl.StartsWith("/uploads/"))
                {
                    _fileStorage.DeleteFile(item.ImageUrl);
                }
                
                _context.GalleryItems.Remove(item);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Ảnh đã được xóa!";
            }
            return RedirectToAction(nameof(Gallery));
        }

        #endregion

        #region Messages Management

        [HttpGet("messages")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Messages(string? search, string? status, DateTime? fromDate, DateTime? toDate, int page = 1)
        {
            const int pageSize = 15;
            var query = _context.ContactMessages.AsQueryable();
            
            // Tìm kiếm theo tên hoặc nội dung
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(m => m.ParentName.Contains(search) || 
                                        m.Phone.Contains(search) ||
                                        (m.Email != null && m.Email.Contains(search)) ||
                                        m.Message.Contains(search));
            }
            
            // Lọc theo trạng thái
            if (!string.IsNullOrEmpty(status))
            {
                var isRead = status == "read";
                query = query.Where(m => m.IsRead == isRead);
            }
            
            // Lọc theo ngày
            if (fromDate.HasValue)
            {
                query = query.Where(m => m.CreatedAt >= fromDate.Value);
            }
            if (toDate.HasValue)
            {
                var endDate = toDate.Value.AddDays(1);
                query = query.Where(m => m.CreatedAt < endDate);
            }
            
            // Mặc định sắp xếp mới nhất lên đầu
            query = query.OrderByDescending(m => m.CreatedAt);
            
            ViewBag.Search = search;
            ViewBag.Status = status;
            ViewBag.FromDate = fromDate?.ToString("yyyy-MM-dd");
            ViewBag.ToDate = toDate?.ToString("yyyy-MM-dd");
            
            var messages = await PaginatedList<ContactMessage>.CreateAsync(query, page, pageSize);
            return View(messages);
        }

        [HttpPost("messages/update-status/{id}")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateMessageStatus(int id, string status)
        {
            var message = await _context.ContactMessages.FindAsync(id);
            if (message != null)
            {
                message.IsRead = status == "Replied" || status == "Closed";
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Messages));
        }

        #endregion

        #region Teachers Management

        [HttpGet("teachers")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Teachers(string? search, string? position, int page = 1)
        {
            const int pageSize = 10;
            var query = _context.Teachers.AsQueryable();
            
            // Tìm kiếm theo tên
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(t => t.FullName.Contains(search) || 
                                        (t.Qualifications != null && t.Qualifications.Contains(search)));
            }
            
            // Lọc theo chức vụ
            if (!string.IsNullOrEmpty(position))
            {
                query = query.Where(t => t.Position == position);
            }
            
            // Mặc định sắp xếp theo tên
            query = query.OrderBy(t => t.FullName);
            
            ViewBag.Search = search;
            ViewBag.Position = position;
            
            // Lấy danh sách chức vụ cho dropdown
            ViewBag.Positions = await _context.Teachers
                .Where(t => !string.IsNullOrEmpty(t.Position))
                .Select(t => t.Position)
                .Distinct()
                .ToListAsync();
            
            var teachers = await PaginatedList<Teacher>.CreateAsync(query, page, pageSize);
            return View(teachers);
        }

        [HttpGet("teachers/create")]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateTeacher()
        {
            return View();
        }

        [HttpPost("teachers/create")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTeacher(Teacher teacher, IFormFile? AvatarUrl_file)
        {
            if (ModelState.IsValid)
            {
                // Xử lý upload ảnh
                if (AvatarUrl_file != null && AvatarUrl_file.Length > 0)
                {
                    try
                    {
                        var imagePath = await _fileStorage.UploadFileAsync(AvatarUrl_file, "teachers");
                        if (!string.IsNullOrEmpty(imagePath))
                        {
                            teacher.AvatarUrl = imagePath;
                        }
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", $"Lỗi upload ảnh: {ex.Message}");
                        return View(teacher);
                    }
                }
                
                _context.Teachers.Add(teacher);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Giáo viên đã được thêm!";
                return RedirectToAction(nameof(Teachers));
            }
            return View(teacher);
        }

        [HttpGet("teachers/edit/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditTeacher(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }
            return View(teacher);
        }

        [HttpPost("teachers/edit/{id}")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTeacher(int id, Teacher teacher, IFormFile? AvatarUrl_file)
        {
            if (id != teacher.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                // Xử lý upload ảnh
                if (AvatarUrl_file != null && AvatarUrl_file.Length > 0)
                {
                    try
                    {
                        var existing = await _context.Teachers.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
                        if (existing != null && !string.IsNullOrEmpty(existing.AvatarUrl) && existing.AvatarUrl.StartsWith("/uploads/"))
                        {
                            _fileStorage.DeleteFile(existing.AvatarUrl);
                        }
                        
                        var imagePath = await _fileStorage.UploadFileAsync(AvatarUrl_file, "teachers");
                        if (!string.IsNullOrEmpty(imagePath))
                        {
                            teacher.AvatarUrl = imagePath;
                        }
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", $"Lỗi upload ảnh: {ex.Message}");
                        return View(teacher);
                    }
                }
                
                _context.Update(teacher);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Giáo viên đã được cập nhật!";
                return RedirectToAction(nameof(Teachers));
            }
            return View(teacher);
        }

        [HttpPost("teachers/delete/{id}")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher != null)
            {
                // Xóa file ảnh nếu là file upload
                if (!string.IsNullOrEmpty(teacher.AvatarUrl) && teacher.AvatarUrl.StartsWith("/uploads/"))
                {
                    _fileStorage.DeleteFile(teacher.AvatarUrl);
                }
                
                _context.Teachers.Remove(teacher);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Giáo viên đã được xóa!";
            }
            return RedirectToAction(nameof(Teachers));
        }

        #endregion

        #region TinyMCE Image Upload API

        [HttpPost("upload-image")]
        [Authorize(Roles = "Admin")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest(new { error = "Không có file" });
            }

            try
            {
                var imagePath = await _fileStorage.UploadFileAsync(file, "editor");
                if (!string.IsNullOrEmpty(imagePath))
                {
                    // Trả về URL tuyệt đối
                    var absoluteUrl = $"{Request.Scheme}://{Request.Host}{imagePath}";
                    return Ok(new { location = absoluteUrl });
                }
                return BadRequest(new { error = "Upload thất bại" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        #endregion

        #region Helpers

        private IActionResult RedirectToLocal(string? returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction(nameof(Index));
        }

        #endregion
    }
}
