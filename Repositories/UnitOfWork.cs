using System.Threading.Tasks;
using MamNonApp.Data;
using MamNonApp.Interfaces;
using MamNonApp.Models.Entities;

namespace MamNonApp.Repositories
{
    /// <summary>
    /// Unit of Work Implementation - Kết nối với Database thật
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private bool _disposed = false;

        public IRepository<Teacher> Teachers { get; private set; }
        public IRepository<EducationProgram> Programs { get; private set; }
        public IRepository<GalleryItem> GalleryItems { get; private set; }
        public IRepository<ContactMessage> ContactMessages { get; private set; }
        public IRepository<SchoolInfo> SchoolInfo { get; private set; }
        public IRepository<Testimonial> Testimonials { get; private set; }
        public IRepository<Activity> Activities { get; private set; }
        public IRepository<News> News { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Teachers = new EfRepository<Teacher>(context);
            Programs = new EfRepository<EducationProgram>(context);
            GalleryItems = new EfRepository<GalleryItem>(context);
            ContactMessages = new EfRepository<ContactMessage>(context);
            SchoolInfo = new EfRepository<SchoolInfo>(context);
            Testimonials = new EfRepository<Testimonial>(context);
            Activities = new EfRepository<Activity>(context);
            News = new EfRepository<News>(context);
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
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
