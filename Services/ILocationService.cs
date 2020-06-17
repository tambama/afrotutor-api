using System.Collections.Generic;
using afrotutor.webapi.Entities;

namespace afrotutor.webapi.Services
{
    public interface ILocationService
    {
        IEnumerable<Location> GetAll();
        IEnumerable<Location> GetUserLocations(int id);
        Location GetById(int id);
        Location Create(Location location);
        void Update(Location location);
        void Delete(int id);
    }
}