using System.Collections.Generic;
using System.Threading.Tasks;
using MamNonApp.Models.Entities;

namespace MamNonApp.Interfaces
{
    /// <summary>
    /// Interface cho Teacher Service - Design Pattern: Dependency Injection + Interface Segregation
    /// </summary>
    public interface ITeacherService
    {
        Task<IEnumerable<Teacher>> GetAllTeachersAsync();
        Task<IEnumerable<Teacher>> GetActiveTeachersAsync();
        Task<Teacher?> GetTeacherByIdAsync(int id);
        Task<IEnumerable<Teacher>> GetFeaturedTeachersAsync(int count = 4);
    }
}
