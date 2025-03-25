using DDVM_Website.Models;
using DDVM_Website.DTOs;

namespace DDVM_Website.Services.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<Event>> GetAllEventsAsync();
        Task<Event> GetEventByIdAsync(int id);
        Task<Event> CreateEventAsync(EventDto eventDto);
        Task<Event> UpdateEventAsync(int id, EventDto eventDto);
        Task<bool> DeleteEventAsync(int id);
        Task SendEmailAsync(string toEmail, string subject, string body);
    }
}
