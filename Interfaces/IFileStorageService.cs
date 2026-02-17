namespace MamNonApp.Interfaces
{
    public interface IFileStorageService
    {
        /// <summary>
        /// Upload file và trả về đường dẫn tương đối
        /// </summary>
        Task<string?> UploadFileAsync(IFormFile file, string folder = "images");
        
        /// <summary>
        /// Xóa file theo đường dẫn
        /// </summary>
        bool DeleteFile(string relativePath);
        
        /// <summary>
        /// Kiểm tra file có phải là ảnh hợp lệ
        /// </summary>
        bool IsValidImage(IFormFile file);
        
        /// <summary>
        /// Lấy đường dẫn tuyệt đối từ đường dẫn tương đối
        /// </summary>
        string GetAbsolutePath(string relativePath);
    }
}
