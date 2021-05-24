using PickAnIcon.Database;
using PickAnIcon.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PickAnIcon.Repositories
{
    public class PartsListRepository : IPartsRepository
    {
        private List<Part> _parts = new List<Part>()
        {
            new Part(){ID = 1, FileName="bg_circle_half.svg", IsFree=true},
            new Part(){ID = 2, FileName="db.svg", IsFree=false},
        };

        public void Add(Part part)
        {
            _parts.Add(part);
        }

        public void Remove(Part part)
        {
            _parts.Remove(part);
        }

        public void Update(Part part)
        {
            var storedPart = _parts.Find(p => p.ID == part.ID);
            if (storedPart != null)
            {
                storedPart.FileName = part.FileName;
                storedPart.IsFree = part.IsFree;
            } 
        }

        public Part GetById(int id)
        {
            return _parts.Find(p => p.ID == id);
        }

        public List<Part> GetAll()
        {
            return _parts;
        }
    }
}
