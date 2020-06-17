using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace afrotutor.webapi.Entities
{
    public class UserClass
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ClassId { get; set; }
        public bool IsPaid { get; set; }
        public string PaymentMethod { get; set; }
        public bool Subscribed { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("ClassId")]
        public virtual Class Class { get; set; }
    }
}