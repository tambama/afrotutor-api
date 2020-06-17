using System;
using System.Collections.Generic;
using afrotutor.webapi.Entities;

namespace afrotutor.webapi.Services
{
    public interface IClassService
    {
        IEnumerable<Class> GetAll();
        IEnumerable<Class> GetTutorClasses(int id);
        Class GetById(int id);
        IEnumerable<Class> SearchClasses(string name);
        IEnumerable<Class> SearchClasses(string name, string location);
        IEnumerable<Class> SearchClasses(string name, DateTime startDate);
        IEnumerable<Class> SearchClasses(string name, DateTime startDate, DateTime endDate);
        IEnumerable<Class> SearchClasses(string name, string location, DateTime startDate);
        IEnumerable<Class> SearchClasses(string name, string location, DateTime startDate, DateTime endDate);
        Class Create(Class Class);
        Class Update(Class Class);
        void Delete(int id);
        Class Cancel(int id);
        Class Activate(int id);
    }
}