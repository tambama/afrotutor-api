using System.Collections.Generic;
using afrotutor.webapi.Entities;

namespace afrotutor.webapi.Services
{
    public interface IUserClassService
    {
        UserClass GetById(int id);
        IEnumerable<UserClass> GetUserClasses(int id);
        UserClass Create(UserClass userClass);
        UserClass Update(UserClass userClass);
        void Delete(int id);
    }
}