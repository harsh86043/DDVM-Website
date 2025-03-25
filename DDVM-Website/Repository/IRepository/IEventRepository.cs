using DDVM_Website.Models;

namespace DDVM_Website.Repository.IRepository
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetAllEventAsync();
        Task<Event> GetEventByIdAsync(int id);
        Task<Event> AddEventAsync(Event newEvent);
        Task<Event> UpdateEventAsync(Event updatedEvent);
        Task<bool> DeleteEventAsync(int id); // Ensure this method exists
    }
}