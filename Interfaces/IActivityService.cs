using System.Collections.Generic;
using System.Threading.Tasks;
using MamNonApp.Models.Entities;

namespace MamNonApp.Interfaces
{
    /// <summary>
    /// Interface cho Activity Service
    /// </summary>
    public interface IActivityService
    {
        Task<IEnumerable<Activity>> GetAllActivitiesAsync();
        Task<IEnumerable<Activity>> GetActiveActivitiesAsync();
        Task<IEnumerable<Activity>> GetFeaturedActivitiesAsync(int count = 6);
        Task<IEnumerable<Activity>> GetActivitiesByCategoryAsync(string category);
        Task<Activity?> GetActivityByIdAsync(int id);
        Task<IEnumerable<string>> GetCategoriesAsync();
        Task IncrementViewCountAsync(int id);
    }
}
