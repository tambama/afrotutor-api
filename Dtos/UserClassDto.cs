namespace afrotutor.webapi.Dtos
{
    public class UserClassDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ClassId { get; set; }
        public bool IsPaid { get; set; }
        public string PaymentMethod { get; set; }
        public bool Subscribed { get; set; }
    }
}