using DDVM_Website.Models;
using DDVM_Website.Repository.IRepository;
using DDVM_Website.Services.Interfaces;
using DDVM_Website.DTOs;

namespace DDVM_Website.Services
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _newsRepository;

        public NewsService(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        public async Task<IEnumerable<News>> GetAllNewsAsync()
        {
            return await _newsRepository.GetAllNewsAsync();
        }

        public async Task<News> GetNewsByIdAsync(int id)
        {
            return await _newsRepository.GetNewsByIdAsync(id);
        }

        public async Task<News> CreateNewsAsync(NewsDto newsDto)
        {
            var news = new News
            {
                Title = newsDto.Title,
                Content = newsDto.Content,
                PublishedDate = newsDto.PublishedDate,
                ImageUrl = newsDto.ImageUrl
            };

            return await _newsRepository.AddNewsAsync(news);
        }

        public async Task<News> UpdateNewsAsync(int id, NewsDto newsDto)
        {
            var existingNews = await _newsRepository.GetNewsByIdAsync(id);
            if (existingNews == null)
            {
                return null; // News not found
            }

            existingNews.Title = newsDto.Title;
            existingNews.Content = newsDto.Content;
            existingNews.PublishedDate = newsDto.PublishedDate;
            existingNews.ImageUrl = newsDto.ImageUrl;

            return await _newsRepository.UpdateNewsAsync(existingNews);
        }

        public async Task<bool> DeleteNewsAsync(int id)
        {
            return await _newsRepository.DeleteNewsAsync(id);
        }
    }
}