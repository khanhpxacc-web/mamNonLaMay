using System.Threading.Tasks;
using MamNonApp.Models.Entities;

namespace MamNonApp.Interfaces
{
    /// <summary>
    /// Interface cho School Info Service
    /// </summary>
    public interface ISchoolInfoService
    {
        Task<SchoolInfo?> GetSchoolInfoAsync();
        Task<Testimonial?> GetTestimonialByIdAsync(int id);
        Task<IEnumerable<Testimonial>> GetFeaturedTestimonialsAsync(int count = 3);
    }
}
