using System.Threading.Tasks;
using MamNonApp.Interfaces;
using MamNonApp.Models.Entities;

namespace MamNonApp.Repositories
{
    /// <summary>
    /// Unit of Work Implementation - Design Pattern: Unit of Work
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private bool _disposed = false;

        public IRepository<Teacher> Teachers { get; private set; }
        public IRepository<EducationProgram> Programs { get; private set; }
        public IRepository<GalleryItem> GalleryItems { get; private set; }
        public IRepository<ContactMessage> ContactMessages { get; private set; }
        public IRepository<SchoolInfo> SchoolInfo { get; private set; }
        public IRepository<Testimonial> Testimonials { get; private set; }
        public IRepository<Activity> Activities { get; private set; }
        public IRepository<News> News { get; private set; }

        public UnitOfWork()
        {
            Teachers = new Repository<Teacher>(InMemoryDataStore.Teachers);
            Programs = new Repository<EducationProgram>(InMemoryDataStore.Programs);
            GalleryItems = new Repository<GalleryItem>(InMemoryDataStore.GalleryItems);
            ContactMessages = new Repository<ContactMessage>(InMemoryDataStore.ContactMessages);
            SchoolInfo = new Repository<SchoolInfo>(InMemoryDataStore.SchoolInfo);
            Testimonials = new Repository<Testimonial>(InMemoryDataStore.Testimonials);
            Activities = new Repository<Activity>(InMemoryDataStore.Activities);
            News = new Repository<News>(InMemoryDataStore.News);
        }

        public async Task<int> CompleteAsync()
        {
            return await Task.FromResult(1);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }
    }
}
