using PickAnIcon.Database;
using PickAnIcon.Database.Entities;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PickAnIcon.Repositories
{
    public interface IUsersRepository
    {
        void Add(User user);
        void Remove(User user);
        void Update(User user);
        User GetById(int id);
        User GetByUsername(string username);

    }

    public class UsersRepository : IUsersRepository
    {
        private AppDbContext _db;

        public UsersRepository(AppDbContext context)
        {
            _db = context;
        }

        public void Add(User user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();
        }

        public void Remove(User user)
        {
            _db.Users.Remove(user);
            _db.SaveChanges();
        }

        public void Update(User user)
        {
            _db.Users.Update(user);
            _db.SaveChanges();
        }

        public User GetById(int id)
        {
            return _db.Users
                .Include(u => u.Icons)
                .FirstOrDefault(u => u.ID == id);
        }

        public User GetByUsername(string username)
        {
            return _db.Users
                .Include(u => u.Icons)
                .FirstOrDefault(u => u.Username == username);
        }    
    }
}
