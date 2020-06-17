using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace afrotutor.webapi.Entities
{
    public class Class
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [Required(ErrorMessage = "Subject Required")]
        public string Subject { get; set; }  
        [Required(ErrorMessage = "Topic Required")]
        public string Topic { get; set; }
        [Required(ErrorMessage = "Description Required")]
        public string Description { get; set; }
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        public int Capacity { get; set; }
        public int Count { get; set; }
        public bool IsFull { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }    
        [Required(ErrorMessage = "Location Name Required")]
        public int LocationId { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsCancelled { get; set; }

        [ForeignKey("LocationId")]
        public virtual Location Location { get; set;}

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}