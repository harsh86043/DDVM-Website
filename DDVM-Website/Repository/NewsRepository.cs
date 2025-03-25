using DDVM_Website.Data;
using DDVM_Website.Models;
using DDVM_Website.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace DDVM_Website.Repository
{
    public class NewsRepository : INewsRepository
    {
        private readonly ApplicationDbContext _context;

        public NewsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<News>> GetAllNewsAsync()
        {
            return await _context.News.ToListAsync();
        }

        public async Task<News> GetNewsByIdAsync(int id)
        {
            return await _context.News.FindAsync(id);
        }

        public async Task<News> AddNewsAsync(News news)
        {
            _context.News.Add(news);
            await _context.SaveChangesAsync();
            return news;
        }

        public async Task<News> UpdateNewsAsync(News news)
        {
            _context.News.Update(news);
            await _context.SaveChangesAsync();
            return news;
        }

        public async Task<bool> DeleteNewsAsync(int id)
        {
            var newsItem = await _context.News.FindAsync(id);
            if (newsItem == null)
                return false;

            _context.News.Remove(newsItem);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}