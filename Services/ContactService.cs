using System.Threading.Tasks;
using MamNonApp.Interfaces;
using MamNonApp.Models.Entities;

namespace MamNonApp.Services
{
    /// <summary>
    /// Contact Service Implementation
    /// </summary>
    public class ContactService : IContactService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ContactService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> SendContactMessageAsync(ContactMessage message)
        {
            try
            {
                await _unitOfWork.ContactMessages.AddAsync(message);
                await _unitOfWork.CompleteAsync();
                
                // Gửi email thông báo
                await SendEmailAsync(
                    "info@mamnonlamay.edu.vn",
                    $"[Liên hệ mới] Từ {message.ParentName}",
                    $"Tên phụ huynh: {message.ParentName}\nSĐT: {message.Phone}\nEmail: {message.Email}\nTên bé: {message.ChildName}\nTuổi: {message.ChildAge}\nNội dung: {message.Message}"
                );
                
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> SendEmailAsync(string to, string subject, string body)
        {
            // Trong thực tế: sử dụng SMTP client hoặc dịch vụ email
            // Demo: chỉ log ra console
            await Task.CompletedTask;
            return true;
        }
    }
}

