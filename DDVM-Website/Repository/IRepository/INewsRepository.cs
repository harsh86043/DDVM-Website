using DDVM_Website.Models;

namespace DDVM_Website.Repository.IRepository
{
    public interface INewsRepository
    {
        Task<IEnumerable<News>> GetAllNewsAsync();
        Task<News> GetNewsByIdAsync(int id);
        Task<News> AddNewsAsync(News news);
        Task<News> UpdateNewsAsync(News news);
        Task<bool> DeleteNewsAsync(int id);
    }
}