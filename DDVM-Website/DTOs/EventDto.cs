﻿namespace DDVM_Website.DTOs
{
    public class EventDto
    {
        public int EventId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
        public string ImageUrl { get; set; }
    }
}