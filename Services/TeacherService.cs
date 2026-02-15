using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MamNonApp.Interfaces;
using MamNonApp.Models.Entities;

namespace MamNonApp.Services
{
    /// <summary>
    /// Teacher Service Implementation - Design Pattern: Service Layer
    /// </summary>
    public class TeacherService : ITeacherService
    {
        private readonly IUnitOfWork _unitOfWork;

        // Dependency Injection qua constructor
        public TeacherService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Teacher>> GetAllTeachersAsync()
        {
            return await _unitOfWork.Teachers.GetAllAsync();
        }

        public async Task<IEnumerable<Teacher>> GetActiveTeachersAsync()
        {
            var teachers = await _unitOfWork.Teachers.GetAllAsync();
            return teachers.Where(t => t.IsActive).OrderByDescending(t => t.ExperienceYears);
        }

        public async Task<Teacher?> GetTeacherByIdAsync(int id)
        {
            return await _unitOfWork.Teachers.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Teacher>> GetFeaturedTeachersAsync(int count = 4)
        {
            var teachers = await _unitOfWork.Teachers.GetAllAsync();
            return teachers
                .Where(t => t.IsActive)
                .OrderByDescending(t => t.ExperienceYears)
                .Take(count);
        }
    }
}

