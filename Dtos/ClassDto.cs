using System;
using afrotutor.webapi.Entities;

namespace afrotutor.webapi.Dtos
{
    public class ClassDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int UserClassId { get; set; }
        public string Tutor { get; set; }
        public string Subject { get; set; } 
        public string Topic { get; set; }
        public string Description { get; set; }
        public bool Subscribed { get; set; }
        public decimal Price { get; set; }
        public int Capacity { get; set; }
        public int Count { get; set; }
        public bool IsFull { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; } 
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public bool IsOwner { get; set; }
        public bool IsCancelled { get; set; }
    }
}