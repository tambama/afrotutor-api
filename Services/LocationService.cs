using System;
using System.Collections.Generic;
using System.Linq;
using afrotutor.webapi.Entities;
using afrotutor.webapi.Helpers;

namespace afrotutor.webapi.Services
{

    public class LocationService : ILocationService
    {
        private DataContext _context;

        public LocationService(DataContext context)
        {
            _context = context;
        }
        public IEnumerable<Location> GetAll()
        {
            return _context.Locations;
        }

        public Location GetById(int id)
        {
            return _context.Locations.Find(id);
        }

        public Location Create(Location location)
        {
            _context.Locations.Add(location);
            _context.SaveChanges();

            return location;
        }

        public void Update(Location locationParam)
        {
            var location = _context.Locations.Find(locationParam.Id);

            if (location == null)
                throw new AppException("Location not found");

            // update user properties
            location.Name = locationParam.Name;
            location.Address = locationParam.Address;
            location.City = locationParam.City;
            location.Country = locationParam.Country;

            _context.Locations.Update(location);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var location = _context.Locations.Find(id);
            if (location != null)
            {
                _context.Locations.Remove(location);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Location> GetUserLocations(int id)
        {
            return _context.Locations.Where(l => l.UserId == id);
        }
    }
}