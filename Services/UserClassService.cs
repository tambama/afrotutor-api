using System;
using System.Collections.Generic;
using System.Linq;
using afrotutor.webapi.Entities;
using afrotutor.webapi.Helpers;
using Microsoft.EntityFrameworkCore;

namespace afrotutor.webapi.Services
{

    public class UserClassService : IUserClassService
    {
        private DataContext _context;

        public UserClassService(DataContext context)
        {
            _context = context;
        }

        public UserClass Create(UserClass userClass)
        {
            UserClass added = new UserClass();

            var @class = _context.Classes.Find(userClass.ClassId);

            if(@class.IsFull)
                throw new AppException("This class is full");
            
            if(@class.UserId == userClass.UserId)
                throw new AppException("Cannot subscribe to your own class");

            var subscribed = _context.UserClasses.FirstOrDefault(c => c.ClassId == userClass.ClassId && c.UserId == userClass.UserId);
            if(subscribed != null){

                if(subscribed.Subscribed)
                    throw new AppException("Already subscribed to this class");

                if(!subscribed.Subscribed){
                    subscribed.Subscribed = true;
                    _context.Entry(subscribed).State = EntityState.Modified;
                }
            } else
            {
                userClass.Subscribed = true;
                _context.UserClasses.Add(userClass);
            }

            @class.Count += 1;
            if(@class.Count == @class.Capacity)
                @class.IsFull = true;

            _context.Entry(@class).State = EntityState.Modified;
            _context.SaveChanges();
            return _context.UserClasses.FirstOrDefault(c => c.ClassId == userClass.ClassId && c.UserId == userClass.UserId);
        }

        public IEnumerable<UserClass> GetUserClasses(int id)
        {
            return _context.UserClasses
                .Include(c => c.Class).ThenInclude(c => c.Location)
                .Include(c => c.Class).ThenInclude(c => c.User)
                .Where(c => c.UserId == id && c.Subscribed && !c.IsDeleted);
        }

        public UserClass Update(UserClass ClassParam)
        {
            var Class = _context.UserClasses.Find(ClassParam.Id);

            bool unsubscribed = false;

            if (Class == null)
                throw new AppException("Class not found");

            if(Class.Subscribed){
                if(!ClassParam.Subscribed){
                    unsubscribed = true;
                }
            }

            // update class properties
            Class.ClassId = ClassParam.ClassId;
            Class.IsPaid = ClassParam.IsPaid;
            Class.PaymentMethod = ClassParam.PaymentMethod;
            Class.Subscribed = ClassParam.Subscribed;

            _context.Entry(Class).State = EntityState.Modified;

            if(unsubscribed){
                var @class = _context.Classes.Find(ClassParam.ClassId);
                @class.Count -= 1;
                if(@class.Count != @class.Capacity)
                    @class.IsFull = false;
                _context.Entry(@class).State = EntityState.Modified;
            }

            _context.SaveChanges();

            return ClassParam;
        }

        public void Delete(int id)
        {
            var Class = _context.UserClasses.Find(id);
            if (Class != null)
            {
                Class.IsDeleted = true;
                _context.Entry(Class).State = EntityState.Modified;
                _context.SaveChanges();
            }

            throw new AppException("Class not found");
        }

        public UserClass GetById(int id)
        {
            return _context.UserClasses.Find(id);
        }
    }
}