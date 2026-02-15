using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MamNonApp.Interfaces;
using MamNonApp.Models.Entities;

namespace MamNonApp.Services
{
    /// <summary>
    /// News Service Implementation
    /// </summary>
    public class NewsService : INewsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public NewsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<News>> GetAllNewsAsync()
        {
            return await _unitOfWork.News.GetAllAsync();
        }

        public async Task<IEnumerable<News>> GetPublishedNewsAsync()
        {
            var news = await _unitOfWork.News.GetAllAsync();
            return news
                .Where(n => n.IsActive && (n.PublishedAt == null || n.PublishedAt <= DateTime.Now))
                .OrderByDescending(n => n.PublishedAt ?? n.CreatedAt);
        }

        public async Task<IEnumerable<News>> GetFeaturedNewsAsync(int count = 4)
        {
            var news = await _unitOfWork.News.GetAllAsync();
            return news
                .Where(n => n.IsActive && n.IsFeatured)
                .OrderByDescending(n => n.PublishedAt ?? n.CreatedAt)
                .Take(count);
        }

        public async Task<IEnumerable<News>> GetLatestNewsAsync(int count = 6)
        {
            var news = await _unitOfWork.News.GetAllAsync();
            return news
                .Where(n => n.IsActive)
                .OrderByDescending(n => n.PublishedAt ?? n.CreatedAt)
                .Take(count);
        }

        public async Task<IEnumerable<News>> GetNewsByCategoryAsync(string category)
        {
            var news = await _unitOfWork.News.GetAllAsync();
            return news
                .Where(n => n.IsActive && n.Category == category)
                .OrderByDescending(n => n.PublishedAt ?? n.CreatedAt);
        }

        public async Task<News?> GetNewsByIdAsync(int id)
        {
            return await _unitOfWork.News.GetByIdAsync(id);
        }

        public async Task<News?> GetNewsBySlugAsync(string slug)
        {
            var news = await _unitOfWork.News.GetAllAsync();
            return news.FirstOrDefault(n => n.Slug == slug && n.IsActive);
        }

        public async Task<IEnumerable<string>> GetCategoriesAsync()
        {
            var news = await _unitOfWork.News.GetAllAsync();
            return news
                .Where(n => n.IsActive && !string.IsNullOrEmpty(n.Category))
                .Select(n => n.Category!)
                .Distinct()
                .ToList();
        }

        public async Task<IEnumerable<News>> SearchNewsAsync(string keyword)
        {
            var news = await _unitOfWork.News.GetAllAsync();
            return news
                .Where(n => n.IsActive && 
                    (n.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                     n.Summary?.Contains(keyword, StringComparison.OrdinalIgnoreCase) == true))
                .OrderByDescending(n => n.PublishedAt ?? n.CreatedAt);
        }

        public async Task IncrementViewCountAsync(int id)
        {
            var news = await _unitOfWork.News.GetByIdAsync(id);
            if (news != null)
            {
                news.ViewCount++;
                _unitOfWork.News.Update(news);
                await _unitOfWork.CompleteAsync();
            }
        }
    }
}

