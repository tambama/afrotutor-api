using System.ComponentModel.DataAnnotations;

namespace afrotutor.webapi.Entities
{
    public class Location
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [Required(ErrorMessage = "Location Name Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Address Required")]
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
    }
}