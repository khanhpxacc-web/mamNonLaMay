using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MamNonApp.Interfaces;
using MamNonApp.Models.Entities;

namespace MamNonApp.Services
{
    /// <summary>
    /// School Info Service Implementation
    /// </summary>
    public class SchoolInfoService : ISchoolInfoService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SchoolInfoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<SchoolInfo?> GetSchoolInfoAsync()
        {
            var info = await _unitOfWork.SchoolInfo.GetAllAsync();
            return info.FirstOrDefault();
        }

        public async Task<Testimonial?> GetTestimonialByIdAsync(int id)
        {
            return await _unitOfWork.Testimonials.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Testimonial>> GetFeaturedTestimonialsAsync(int count = 3)
        {
            var testimonials = await _unitOfWork.Testimonials.GetAllAsync();
            return testimonials
                .Where(t => t.IsActive && t.IsFeatured)
                .OrderBy(t => t.DisplayOrder)
                .Take(count);
        }
    }
}

