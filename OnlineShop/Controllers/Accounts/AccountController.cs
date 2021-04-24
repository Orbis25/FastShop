using BussinesLayer.UnitOfWork;
using DataLayer.Enums.Base;
using DataLayer.ViewModels.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Controllers.Base;
using OnlineShop.ExtensionMethods;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnlineShop.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUnitOfWork _services;
        private readonly IWebHostEnvironment _env;

        public AccountController(IUnitOfWork services, IWebHostEnvironment environment)
        {
            _env = environment;
            _services = services;
        }

        [Authorize(Roles = nameof(AuthLevel.Admin))]
        public async Task<IActionResult> BlockOrUnlockAccount(Guid id)
        {
            await _services.AccountService.BlockAndUnlockAccount(id);
            return RedirectToAction("Users", "Admin");
        }

        [Authorize(Roles = nameof(AuthLevel.Admin) + "," + nameof(AuthLevel.User))]
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Index", "Home");
            var model = await _services.AccountService.GetByEmail(User.Identity.Name);
            return Ok(model);
        }

        [HttpGet]
        public IActionResult EmailConfirm() => View();

        [HttpGet]
        public async Task<IActionResult> Validate(string code)
        {
            if (!string.IsNullOrEmpty(code))
            {
                if (await _services.AccountService.ValidateUser(code))
                {
                    ViewData["validate"] = true;
                }
            }
            return View();
        }

        [Authorize(Roles = nameof(AuthLevel.Admin)+","+nameof(AuthLevel.User))]
        [HttpPost]
        public async Task<IActionResult> UploadImageProfile(UserUploadImageVM model)
        {
            if (model.Img != null && !string.IsNullOrEmpty(model.Id))
            {
                var user = await _services.UserService.Get(model.Id);
                if (user == null) return new NotFoundView();
                var result = await _services.ImageServerService
                    .UploadImage(model.Img, _env.WebRootPath, nameof(User),user.ProfileImage);
                user.ProfileImage = result;
                await _services.UserService.Update(user);
            }

            return Redirect("/Identity/Account/Manage");
        }

        [Authorize(Roles = nameof(AuthLevel.Admin))]
        [HttpGet]
        public async Task<IActionResult> GetImageProfile()
        {
            var id = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            if (id == null) return BadRequest("");
            var user = await _services.UserService.Get(id);
            return Ok(user.ProfileImage);
        }

        [Authorize(Roles = nameof(AuthLevel.Admin))]
        [HttpGet]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _services.UserService.Get(id);
            if (result == null) return BadRequest("Usuario no encontrado");
            return PartialView("_UserDetailPartial",result);
        }
    }
}