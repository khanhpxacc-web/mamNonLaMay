using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MamNonApp.Interfaces;
using MamNonApp.Models.Entities;

namespace MamNonApp.Services
{
    /// <summary>
    /// Gallery Service Implementation
    /// </summary>
    public class GalleryService : IGalleryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GalleryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<GalleryItem>> GetAllGalleryItemsAsync()
        {
            return await _unitOfWork.GalleryItems.GetAllAsync();
        }

        public async Task<IEnumerable<GalleryItem>> GetFeaturedGalleryItemsAsync(int count = 6)
        {
            var items = await _unitOfWork.GalleryItems.GetAllAsync();
            return items
                .Where(g => g.IsActive && g.IsFeatured)
                .OrderBy(g => g.DisplayOrder)
                .Take(count);
        }

        public async Task<IEnumerable<GalleryItem>> GetGalleryItemsByCategoryAsync(string category)
        {
            var items = await _unitOfWork.GalleryItems.GetAllAsync();
            return items
                .Where(g => g.IsActive && g.Category == category)
                .OrderBy(g => g.DisplayOrder);
        }

        public async Task<GalleryItem?> GetGalleryItemByIdAsync(int id)
        {
            return await _unitOfWork.GalleryItems.GetByIdAsync(id);
        }

        public async Task<IEnumerable<string>> GetCategoriesAsync()
        {
            var items = await _unitOfWork.GalleryItems.GetAllAsync();
            return items
                .Where(g => g.IsActive && !string.IsNullOrEmpty(g.Category))
                .Select(g => g.Category!)
                .Distinct()
                .ToList();
        }
    }
}

