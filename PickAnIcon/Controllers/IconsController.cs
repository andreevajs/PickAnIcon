using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PickAnIcon.Models;
using PickAnIcon.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PickAnIcon.Controllers
{
    public class IconsController : Controller
    {
        private protected ILogger _logger;
        private protected IConfiguration _configuration;
        private protected IIconsService _iconsService;

        public IconsController(ILogger<AccountController> logger, IConfiguration configuration, IIconsService iconsService)
        {
            _logger = logger;
            _configuration = configuration;
            _iconsService = iconsService;
        }
        public IActionResult List()
        {
            return View(_iconsService.GetAllIcons());
        }

        [Authorize]
        public IActionResult My()
        {
            return View(_iconsService.GetIconsByUser(HttpContext.User.Identity.Name));
        }

        [Authorize]
        [HttpGet(template: "/Icons/Editor")]
        public IActionResult Editor()
        {
            ViewBag.PartsCount = _configuration.GetSection("PickAnIcon").GetSection("Editor").GetSection("MaxPartsInRow").Value;
            ViewBag.Parts = _iconsService.GetAllParts();
            return View(new IconViewModel()
            {
                Name = "My new icon name",
                Parts = new List<IconPartViewModel>()
            });
        }

        [Authorize]
        [HttpGet(template:"/Icons/Editor/{id}")]
        public IActionResult Editor(int id)
        {
            var user = HttpContext.User.Identity.Name;
            var icon = _iconsService.GetIconsByUser(user).Find(x => x.Id == id);

            if (icon == null) {
                _logger.LogDebug("Icon with id {0} was not found. Username {1}", id, user);
                return Content("icon was not found");
            }

            ViewBag.PartsCount = _configuration.GetSection("PickAnIcon").GetSection("Editor").GetSection("MaxPartsInRow").Value;
            ViewBag.Parts = _iconsService.GetAllParts();

            return View(icon);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Editor(IconViewModel model)
        {
            List<int> selectedParts = HttpContext.Request.Form["parts"]
                .Where(x => x.All(char.IsDigit))
                .Select(x => int.Parse(x)).ToList();

            var user = HttpContext.User.Identity.Name;

            Result<int> result;
            if (model.Id == 0)
            {
                result = _iconsService.AddIcon(model, selectedParts, user);
                _logger.LogInformation("New icon created, with id {0}. Username {1}", result.Value, user);
            }
            else
            {
                result = _iconsService.UpdateIcon(model, selectedParts, user);
                _logger.LogInformation("Icon was updated, icon id {0}. Username {1}", result.Value, user);
            }

            if (result.HasErrors)
            {
                return Content(result.ErrorMessage);
            }

            return Redirect("/Icons/Editor/" + result.Value);
        }
    }
}
