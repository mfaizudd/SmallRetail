using BC = BCrypt.Net.BCrypt;
using System;
using System.Collections.Generic;
using System.Linq;
using SmallRetail.Data;
using SmallRetail.Data.Models;

namespace SmallRetail.Services
{
    public class UserService : IUserService
    {
        private readonly SmallRetailDbContext _db;
        public int Count => _db.Users.Count();

        public UserService(SmallRetailDbContext db)
        {
            _db = db;
        }

        public IEnumerable<User> GetAll(int limit = 10, int page = 1)
        {
            var query = _db.Users.OrderBy(u => u.Name);
            if (limit == 0)
                return query;

            return query
                .Skip(limit * (page - 1))
                .Take(limit);
        }

        public User? Get(params object[] keyValues)
        {
            return _db.Users.Find(keyValues);
        }

        public User? Login(string username, string password)
        {
            var user = _db.Users.SingleOrDefault(u => u.Username == username);
            if (user == null) return null;
            if (!BC.Verify(password, user.Password)) return null;
            return user;
        }

        public User? Find(Func<User, bool> predicate)
        {
            return _db.Users.FirstOrDefault(predicate);
        }

        public IEnumerable<User> Where(Func<User, bool> predicate)
        {
            return _db.Users.Where(predicate);
        }

        public void Create(User entity)
        {
            entity.Password = BC.HashPassword(entity.Password);
            entity.DateCreated = DateTime.UtcNow;
            entity.DateUpdated = DateTime.UtcNow;
            _db.Users.Add(entity);
            _db.SaveChanges();
        }

        public void Update(User entity, params object[] keyValues)
        {
            if (keyValues == null)
                throw new ArgumentNullException(nameof(keyValues));
            if (keyValues.Length == 0)
                throw new ArgumentException("Key isn't specified", nameof(keyValues));

            var user = _db.Users.Find(keyValues);
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