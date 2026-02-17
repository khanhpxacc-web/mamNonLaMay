using MamNonApp.Interfaces;

namespace MamNonApp.Services
{
    public class FileStorageService : IFileStorageService
    {
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<FileStorageService> _logger;
        private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".webp", ".bmp" };
        private readonly long _maxFileSize = 5 * 1024 * 1024; // 5MB

        public FileStorageService(IWebHostEnvironment environment, ILogger<FileStorageService> logger)
        {
            _environment = environment;
            _logger = logger;
        }

        public async Task<string?> UploadFileAsync(IFormFile file, string folder = "images")
        {
            if (file == null || file.Length == 0)
                return null;

            if (!IsValidImage(file))
            {
                _logger.LogWarning("Invalid file type attempted: {FileName}", file.FileName);
                throw new InvalidOperationException("Chỉ chấp nhận file ảnh (jpg, jpeg, png, gif, webp, bmp)");
            }

            if (file.Length > _maxFileSize)
            {
                _logger.LogWarning("File too large: {FileSize} bytes", file.Length);
                throw new InvalidOperationException("File ảnh không được vượt quá 5MB");
            }

            try
            {
                // Tạo thư mục uploads nếu chưa tồn tại
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads", folder);
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Tạo tên file unique
                var fileName = $"{Guid.NewGuid():N}{Path.GetExtension(file.FileName).ToLower()}";
                var filePath = Path.Combine(uploadsFolder, fileName);

                // Lưu file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Trả về đường dẫn tương đối
                var relativePath = $"/uploads/{folder}/{fileName}";
                _logger.LogInformation("File uploaded successfully: {FilePath}", relativePath);
                
                return relativePath;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading file: {FileName}", file.FileName);
                throw;
            }
        }

        public bool DeleteFile(string relativePath)
        {
            if (string.IsNullOrEmpty(relativePath))
                return false;

            try
            {
                // Chỉ xóa file trong thư mục uploads
                if (!relativePath.StartsWith("/uploads/"))
                    return false;

                var filePath = Path.Combine(_environment.WebRootPath, relativePath.TrimStart('/'));
                
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    _logger.LogInformation("File deleted: {FilePath}", relativePath);
                    return true;
                }
                
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting file: {FilePath}", relativePath);
                return false;
            }
        }

        public bool IsValidImage(IFormFile file)
        {
            if (file == null) return false;
            
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            return _allowedExtensions.Contains(extension);
        }

        public string GetAbsolutePath(string relativePath)
        {
            if (string.IsNullOrEmpty(relativePath))
                return string.Empty;

            return Path.Combine(_environment.WebRootPath, relativePath.TrimStart('/'));
        }
    }
}
