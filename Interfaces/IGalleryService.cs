using System.Collections.Generic;
using System.Threading.Tasks;
using MamNonApp.Models.Entities;

namespace MamNonApp.Interfaces
{
    /// <summary>
    /// Interface cho Gallery Service
    /// </summary>
    public interface IGalleryService
    {
        Task<IEnumerable<GalleryItem>> GetAllGalleryItemsAsync();
        Task<IEnumerable<GalleryItem>> GetFeaturedGalleryItemsAsync(int count = 6);
        Task<IEnumerable<GalleryItem>> GetGalleryItemsByCategoryAsync(string category);
        Task<GalleryItem?> GetGalleryItemByIdAsync(int id);
        Task<IEnumerable<string>> GetCategoriesAsync();
    }
}
