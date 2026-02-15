using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MamNonApp.Interfaces;
using MamNonApp.Models.Entities;

namespace MamNonApp.Services
{
    /// <summary>
    /// Activity Service Implementation
    /// </summary>
    public class ActivityService : IActivityService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ActivityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Activity>> GetAllActivitiesAsync()
        {
            return await _unitOfWork.Activities.GetAllAsync();
        }

        public async Task<IEnumerable<Activity>> GetActiveActivitiesAsync()
        {
            var activities = await _unitOfWork.Activities.GetAllAsync();
            return activities.OrderByDescending(a => a.EventDate ?? a.CreatedAt);
        }

        public async Task<IEnumerable<Activity>> GetFeaturedActivitiesAsync(int count = 6)
        {
            var activities = await _unitOfWork.Activities.GetAllAsync();
            return activities
                .Where(a => a.IsActive && a.IsFeatured)
                .OrderBy(a => a.DisplayOrder)
                .Take(count);
        }

        public async Task<IEnumerable<Activity>> GetActivitiesByCategoryAsync(string category)
        {
            var activities = await _unitOfWork.Activities.GetAllAsync();
            return activities
                .Where(a => a.IsActive && a.Category == category)
                .OrderByDescending(a => a.EventDate ?? a.CreatedAt);
        }

        public async Task<Activity?> GetActivityByIdAsync(int id)
        {
            return await _unitOfWork.Activities.GetByIdAsync(id);
        }

        public async Task<IEnumerable<string>> GetCategoriesAsync()
        {
            var activities = await _unitOfWork.Activities.GetAllAsync();
            return activities
                .Where(a => a.IsActive && !string.IsNullOrEmpty(a.Category))
                .Select(a => a.Category!)
                .Distinct()
                .ToList();
        }

        public async Task IncrementViewCountAsync(int id)
        {
            var activity = await _unitOfWork.Activities.GetByIdAsync(id);
            if (activity != null)
            {
                activity.ViewCount++;
                _unitOfWork.Activities.Update(activity);
                await _unitOfWork.CompleteAsync();
            }
        }
    }
}

