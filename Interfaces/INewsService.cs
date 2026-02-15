using System.Collections.Generic;
using System.Threading.Tasks;
using MamNonApp.Models.Entities;

namespace MamNonApp.Interfaces
{
    /// <summary>
    /// Interface cho News Service
    /// </summary>
    public interface INewsService
    {
        Task<IEnumerable<News>> GetAllNewsAsync();
        Task<IEnumerable<News>> GetPublishedNewsAsync();
        Task<IEnumerable<News>> GetFeaturedNewsAsync(int count = 4);
        Task<IEnumerable<News>> GetLatestNewsAsync(int count = 6);
        Task<IEnumerable<News>> GetNewsByCategoryAsync(string category);
        Task<News?> GetNewsByIdAsync(int id);
        Task<News?> GetNewsBySlugAsync(string slug);
        Task<IEnumerable<string>> GetCategoriesAsync();
        Task<IEnumerable<News>> SearchNewsAsync(string keyword);
        Task IncrementViewCountAsync(int id);
    }
}
