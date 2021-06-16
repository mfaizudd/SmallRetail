using System;
using System.Collections.Generic;
using SmallRetail.Data;
using SmallRetail.Data.Models;

namespace SmallRetail.Services
{
    public class UserService : IUserService
    {
        private readonly SmallRetailDbContext _db;

        public UserService(SmallRetailDbContext db)
        {
            _db = db;
        }
        
        public IEnumerable<User> GetAll()
        {
            return _db.Users;
        }

        public User Get(params object[] keyValues)
        {
            return _db.Users.Find(keyValues);
        }

        public void Create(User entity)
        {
            entity.DateCreated = DateTime.UtcNow;
            entity.DateUpdated = DateTime.UtcNow;
            _db.Users.Add(entity);
            _db.SaveChanges();
        }

        public void Update(User entity)
        {
            var user = _db.Users.Find(entity.Id);
            if (user == null)
                throw new ArgumentException($"User with id {entity.Id} not found", nameof(entity));

            user.Name = entity.Name;
            user.DateUpdated = DateTime.UtcNow;
            _db.Users.Update(user);
            _db.SaveChanges();
        }

        public void Delete(params object[] keyValues)
        {
            var user = _db.Users.Find(keyValues);
            if (user == null)
                throw new ArgumentException($"User with id {keyValues} not found", nameof(keyValues));

            _db.Users.Remove(user);
            _db.SaveChanges();
        }
    }
}