using DDVM_Website.Helpers;
using DDVM_Website.Models;
using DDVM_Website.Repository.IRepository;
using DDVM_Website.Services.Interfaces;
using DDVM_Website.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DDVM_Website.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IEmailService _emailService; // Added email service for event notifications

        public EventService(IEventRepository eventRepository, IEmailService emailService)
        {
            _eventRepository = eventRepository;
            _emailService = emailService;
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            return await _eventRepository.GetAllEventAsync();
        }

        public async Task<Event> GetEventByIdAsync(int id)
        {
            return await _eventRepository.GetEventByIdAsync(id);
        }

        public async Task<Event> CreateEventAsync(EventDto eventDto)
        {
            var newEvent = new Event
            {
                Title = eventDto.Title,
                Description = eventDto.Description,
                EventDate = eventDto.EventDate,
                ImageUrl = eventDto.ImageUrl
            };

            newEvent = await _eventRepository.AddEventAsync(newEvent);

            return new Event
            {
                EventId = newEvent.EventId,
                Title = newEvent.Title,
                Description = newEvent.Description,
                EventDate = newEvent.EventDate,
                ImageUrl = newEvent.ImageUrl
            };
        }

        public async Task<Event> UpdateEventAsync(int id, EventDto eventDto)
        {
            var existingEvent = await _eventRepository.GetEventByIdAsync(id);
            if (existingEvent == null)
            {
                return null; // Event not found
            }

            existingEvent.Title = eventDto.Title;
            existingEvent.Description = eventDto.Description;
            existingEvent.EventDate = eventDto.EventDate; 
            existingEvent.ImageUrl = eventDto.ImageUrl;

            return await _eventRepository.UpdateEventAsync(existingEvent);
        }

        public async Task<bool> DeleteEventAsync(int id)
        {
            var eventToDelete = await _eventRepository.GetEventByIdAsync(id);
            if (eventToDelete == null)
            {
                return false; // Event does not exist
            }

            return await _eventRepository.DeleteEventAsync(id);
        }

        public Task SendEmailAsync(string toEmail, string subject, string body)
        {
            throw new NotImplementedException();
        }
    }
}