using PickAnIcon.Database;
using PickAnIcon.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PickAnIcon.Repositories
{
    public interface IPartsRepository
    {
        void Add(Part part);
        void Remove(Part part);
        void Update(Part part);
        Part GetById(int id);
        List<Part> GetAll();
    }

    public class PartsRepository : IPartsRepository
    {
        private AppDbContext _db;
        public PartsRepository(AppDbContext context)
        {
            _db = context;
        }

        public void Add(Part part)
        {
            _db.Parts.Add(part);
            _db.SaveChanges();
        }

        public void Remove(Part part)
        {
            _db.Parts.Remove(part);
            _db.SaveChanges();
        }

        public void Update(Part part)
        {
            _db.Parts.Update(part);
            _db.SaveChanges();
        }

        public Part GetById(int id)
        {
            return _db.Parts.FirstOrDefault(i => i.ID == id);
        }

        public List<Part> GetAll()
        {
            return _db.Parts.ToList();
        }
    }
}
