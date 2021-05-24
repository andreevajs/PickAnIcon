using PickAnIcon.Database.Entities;
using PickAnIcon.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using PickAnIcon.Models;

namespace PickAnIcon.Services
{
    public interface IIconsService
    {
        Result<int> AddIcon(IconViewModel icon, List<int> partsIds, string username);
        List<IconViewModel> GetIconsByUser(string username);
        List<IconViewModel> GetAllIcons();
        List<PartViewModel> GetAllParts();
        Icon GetById(int icon);
        Result<int> UpdateIcon(IconViewModel icon, List<int> partsIds, string username);
    }

    public class IconsService : IIconsService
    {
        private IIconsRepository _icons;
        private IPartsRepository _parts;
        private IUsersRepository _users;

        public IconsService(IIconsRepository iconsRepository, IPartsRepository partsRepository, IUsersRepository usersRepository)
        {
            _icons = iconsRepository;
            _parts = partsRepository;
            _users = usersRepository;
        }

        public Result<int> AddIcon(IconViewModel model, List<int> partsIds, string username)
        {
            var user = _users.GetByUsername(username);
            if (user == null)
                return Result<int>.Error("permission denied");
            if (string.IsNullOrWhiteSpace(model.Name))
                model.Name = "Icon";

            var icon = new Icon()
            {
                Name = model.Name,
                Created = DateTime.Now,
                LastEdited = DateTime.Now,
                Owner = user,
                IconParts = new List<IconPart>()
            };

            icon.IconParts.AddRange(partsIds.Select(id => new IconPart() { Icon = icon, Part = _parts.GetById(id) }));

            _icons.Add(icon);

            return Result<int>.Success().WithValue(icon.ID);
        }

        public List<PartViewModel> GetAllParts()
        {
            return _parts.GetAll()
                .Select(ToModel)
                .ToList();
        }

        public Icon GetById(int iconId)
        {
            return _icons.GetById(iconId);
        }

        public List<IconViewModel> GetIconsByUser(string username)
        {
            return _icons.GetByUser(username)
                .Select(ToModel)
                .ToList();
        }

        public List<IconViewModel> GetAllIcons()
        {
            return _icons.GetAll()
                .Select(ToModel)
                .ToList();
        }

        public Result<int> UpdateIcon(IconViewModel icon, List<int> partsIds , string username)
        {
            var dbIcon = GetById(icon.Id);
            if (dbIcon == null)
                return Result<int>.Error("icon doesn't exist");
            else if (dbIcon.Owner.Username != username)
                return Result<int>.Error("permission denied");

            if (!string.IsNullOrEmpty(icon.Name))
                dbIcon.Name = icon.Name;

            dbIcon.LastEdited = DateTime.Now;
            dbIcon.IconParts.RemoveAll(ip => !partsIds.Contains(ip.Part.ID));
            var toAdd = partsIds.Where(id => dbIcon.IconParts.Find(ip => ip.Part.ID == id) == null);
            dbIcon.IconParts.AddRange(toAdd.Select(id => new IconPart() { Icon = dbIcon, Part = _parts.GetById(id) }));

            _icons.Update(dbIcon);

            return Result<int>.Success().WithValue(dbIcon.ID);
        }

        private IconViewModel ToModel(Icon icon)
        {
            return new IconViewModel()
            {
                Id = icon.ID,
                Name = icon.Name,
                LastEdited = icon.LastEdited,
                Parts = icon.IconParts.Select(ToModel).ToList()
            };
        }

        private PartViewModel ToModel(Part part)
        {
            return new PartViewModel
            {
                Id = part.ID,
                FileName = part.FileName,
                IsFree = part.IsFree
            };
        }

        private IconPartViewModel ToModel(IconPart part)
        {
            return new IconPartViewModel
            {
                Id = part.ID,
                PartId = part.Part.ID,
                FileName = part.Part.FileName,
                Layer = part.Layer
            };
        }
    }
}
