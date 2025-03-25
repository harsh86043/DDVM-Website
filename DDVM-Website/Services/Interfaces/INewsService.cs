using DDVM_Website.Models;
using DDVM_Website.DTOs;

namespace DDVM_Website.Services.Interfaces
{
    public interface INewsService
    {
        Task<IEnumerable<News>> GetAllNewsAsync();
        Task<News> GetNewsByIdAsync(int id);
        Task<News> CreateNewsAsync(NewsDto newsDto);
        Task<News> UpdateNewsAsync(int id, NewsDto newsDto);
        Task<bool> DeleteNewsAsync(int id);
    }
}