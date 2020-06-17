using System;
using System.Collections.Generic;
using System.Linq;
using afrotutor.webapi.Entities;
using afrotutor.webapi.Helpers;
using Microsoft.EntityFrameworkCore;

namespace afrotutor.webapi.Services
{

    public class ClassService : IClassService
    {
        private DataContext _context;

        public ClassService(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<Class> GetAll()
        {
            return _context.Classes
                .Include(c => c.Location)
                    .Include(c => c.User)
                        .Where(c => !c.IsDeleted)
                            .ToList();
        }

        public Class GetById(int id)
        {
            return _context.Classes
                .Include(c => c.Location)
                    .Include(c => c.User)
                        .FirstOrDefault(c => c.Id == id);
        }

        public Class Create(Class Class)
        {
            _context.Classes.Add(Class);
            _context.SaveChanges();

            var @class = _context.Classes
                .Include(c => c.Location)
                .Include(c => c.User)
                .FirstOrDefault(c => c.Id == Class.Id);

            return @class;
        }

        public Class Update(Class ClassParam)
        {
            var Class = _context.Classes.Find(ClassParam.Id);

            if (Class == null)
                throw new AppException("Class not found");

            // update class properties
            Class.Subject = ClassParam.Subject;
            Class.Description = ClassParam.Description;
            Class.Price = ClassParam.Price;
            Class.StartTime = Class.StartTime;
            Class.EndTime = Class.EndTime;
            Class.LocationId = ClassParam.LocationId;

            _context.Classes.Update(Class);
            _context.SaveChanges();

            return _context.Classes
                .Include(c => c.Location)
                    .Include(c => c.User)
                        .FirstOrDefault(c => c.Id == ClassParam.Id);
        }

        public Class Cancel(int id)
        {
            var Class = _context.Classes.Find(id);
            if (Class != null)
            {
                Class.IsCancelled = true;
                _context.Entry(Class).State = EntityState.Modified;
                _context.SaveChanges();

                return _context.Classes.Find(id);
            }

            throw new AppException("Class not found");
        }

        public Class Activate(int id)
        {
            var Class = _context.Classes.Find(id);
            if (Class != null)
            {
                Class.IsCancelled = false;
                _context.Entry(Class).State = EntityState.Modified;
                _context.SaveChanges();

                return _context.Classes.Find(id);
            }

            throw new AppException("Class not found");
        }

        public void Delete(int id)
        {
            var Class = _context.Classes.Find(id);
            if (Class != null)
            {
                Class.IsDeleted = true;
                _context.Entry(Class).State = EntityState.Modified;
                _context.SaveChanges();
            } else {
                throw new AppException("Class not found");
            }

            
        }

        public IEnumerable<Class> GetTutorClasses(int id)
        {
            return _context.Classes
                .Include(c => c.Location)
                    .Include(c => c.User)
                        .Where(c => c.UserId == id && !c.IsDeleted);
        }

        public IEnumerable<Class> SearchClasses(string name)
        {
            return _context.Classes.Include(c => c.Location).Include(c => c.User).Where(c => 
                (c.Subject.ToLower().Contains(name.ToLower())
                || c.Topic.ToLower().Contains(name.ToLower())
                || c.Description.ToLower().Contains(name.ToLower()))
                && !c.IsDeleted
                && !c.IsCancelled);
        }

        public IEnumerable<Class> SearchClasses(string name, DateTime startDate)
        {
            return _context.Classes.Include(c => c.Location).Include(c => c.User).Where(c => 
                (c.Subject.ToLower().Contains(name.ToLower())
                || c.Topic.ToLower().Contains(name.ToLower())
                || c.Description.ToLower().Contains(name.ToLower()))
                && c.StartTime.Date == startDate.Date
                && !c.IsDeleted
                && !c.IsCancelled);
        }

        public IEnumerable<Class> SearchClasses(string name, DateTime startDate, DateTime endDate)
        {
            return _context.Classes.Include(c => c.Location).Include(c => c.User).Where(c => 
                (c.Subject.ToLower().Contains(name.ToLower())
                || c.Topic.ToLower().Contains(name.ToLower())
                || c.Description.ToLower().Contains(name.ToLower()))
                && c.StartTime.Date >= startDate.Date
                && c.StartTime.Date <= endDate.Date
                && !c.IsDeleted
                && !c.IsCancelled);
        }

        public IEnumerable<Class> SearchClasses(string name, string location)
        {
            return _context.Classes.Include(c => c.Location).Include(c => c.User).Where(c => 
                (c.Subject.ToLower().Contains(name.ToLower())
                || c.Topic.ToLower().Contains(name.ToLower())
                || c.Description.ToLower().Contains(name.ToLower()))
                && (c.Location.Address.ToLower().Contains(location.ToLower())
                || c.Location.City.ToLower().Contains(location.ToLower())
                || c.Location.Country.ToLower().Contains(location.ToLower()))
                && !c.IsDeleted
                && !c.IsCancelled);
        }

        public IEnumerable<Class> SearchClasses(string name, string location, DateTime startDate)
        {
            return _context.Classes.Include(c => c.Location).Include(c => c.User).Where(c => 
                (c.Subject.ToLower().Contains(name.ToLower())
                || c.Topic.ToLower().Contains(name.ToLower())
                || c.Description.ToLower().Contains(name.ToLower()))
                && (c.Location.Address.ToLower().Contains(location.ToLower())
                || c.Location.City.ToLower().Contains(location.ToLower())
                || c.Location.Country.ToLower().Contains(location.ToLower()))
                && c.StartTime.Date == startDate.Date
                && !c.IsDeleted
                && !c.IsCancelled);
        }

        public IEnumerable<Class> SearchClasses(string name, string location, DateTime startDate, DateTime endDate)
        {
            return _context.Classes.Include(c => c.Location).Include(c => c.User).Where(c => 
                (c.Subject.ToLower().Contains(name.ToLower())
                || c.Topic.ToLower().Contains(name.ToLower())
                || c.Description.ToLower().Contains(name.ToLower()))
                && (c.Location.Address.ToLower().Contains(location.ToLower())
                || c.Location.City.ToLower().Contains(location.ToLower())
                || c.Location.Country.ToLower().Contains(location.ToLower()))
                && c.StartTime.Date >= startDate.Date
                && c.StartTime.Date <= endDate.Date
                && !c.IsDeleted
                && !c.IsCancelled);
        }

        public UserClass AddUserClass(UserClass userClass)
        {
            _context.UserClasses.Add(userClass);
            _context.SaveChanges();
            return userClass;
        }
    }
}