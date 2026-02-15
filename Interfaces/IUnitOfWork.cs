using System;
using System.Threading.Tasks;
using MamNonApp.Models.Entities;

namespace MamNonApp.Interfaces
{
    /// <summary>
    /// Unit of Work Interface - Design Pattern: Unit of Work
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Teacher> Teachers { get; }
        IRepository<EducationProgram> Programs { get; }
        IRepository<GalleryItem> GalleryItems { get; }
        IRepository<ContactMessage> ContactMessages { get; }
        IRepository<SchoolInfo> SchoolInfo { get; }
        IRepository<Testimonial> Testimonials { get; }
        IRepository<Activity> Activities { get; }
        IRepository<News> News { get; }

        Task<int> CompleteAsync();
    }
}
