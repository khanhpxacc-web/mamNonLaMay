using System.Threading.Tasks;
using MamNonApp.Models.Entities;

namespace MamNonApp.Interfaces
{
    /// <summary>
    /// Interface cho Contact Service
    /// </summary>
    public interface IContactService
    {
        Task<bool> SendContactMessageAsync(ContactMessage message);
        Task<bool> SendEmailAsync(string to, string subject, string body);
    }
}
