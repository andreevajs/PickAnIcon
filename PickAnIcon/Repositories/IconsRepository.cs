using Microsoft.EntityFrameworkCore;
using PickAnIcon.Database;
using PickAnIcon.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PickAnIcon.Repositories
{
    public interface IIconsRepository
    {
        void Add(Icon icon);
        void Remove(Icon icon);
        void Update(Icon icon);
        Icon GetById(int id);
        List<Icon> GetAll();
        List<Icon> GetByUser(string username);
    }

    public class IconsRepository : IIconsRepository
    {
        private AppDbContext _db;
        public IconsRepository(AppDbContext context)
        {
            _db = context;
        }

        public void Add(Icon icon)
        {
            _db.Icons.Add(icon);
            _db.SaveChanges();
        }

        public void Remove(Icon icon)
        {
            _db.Icons.Remove(icon);
            _db.SaveChanges();
        }

        public void Update(Icon icon)
        {
            _db.Icons.Update(icon);
            _db.SaveChanges();
        }

        public Icon GetById(int id)
        {
            return _db.Icons
                .Include(i => i.Owner)
                .Include(i => i.IconParts)
                    .ThenInclude(ip => ip.Part)
                .FirstOrDefault(i => i.ID == id);
        }

        public List<Icon> GetAll()
        {
            return _db.Icons.ToList();
        }

        public List<Icon> GetByUser(string username)
        {
            return _db.Icons
                .Include(i => i.IconParts)
                    .ThenInclude(ip => ip.Part)
                .Where(i => i.Owner.Username == username)
                .ToList();
        }
    }
}
