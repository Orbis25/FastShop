using BussinesLayer.UnitOfWork;
using DataLayer.Enums.Base;
using DataLayer.Models.Countries;
using DataLayer.Utils.Paginations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Controllers.Countries
{
    [Authorize(Roles = nameof(AuthLevel.Admin))]
    public class CountryController : Controller
    {
        private IUnitOfWork _services;
        private IWebHostEnvironment _env;
        public CountryController(IUnitOfWork services , IWebHostEnvironment env)
        {
            _services = services;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> Index(PaginationBase paginationBase, string name)
        {
            var result = await _services.CountryService.GetAllPaginated(paginationBase, !string.IsNullOrEmpty(name) ? (x => x.Name.Contains(name) || x.Iso3.Contains(name)) : null);
            return PartialView("_IndexPartial", result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AvaibleCountry model)
        {
            var exist = _services.CountryService.GetAll().Any(x => x.Iso3 == model.Iso3);
            if (exist) return BadRequest("Este pais ya esta en la lista");
            var result = await _services.CountryService.Add(model);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            var result = await _services.CountryService.Remove(id);
            if (!result) return BadRequest("Intente de nuevo mas tarde");
            return Ok(result);
        }

        #region CITY

        [HttpGet]
        public IActionResult GetAllCities(string countryCode)
        {
            if (string.IsNullOrEmpty(countryCode)) return BadRequest("Codigo de pais invalido");
            var result = _services.CityService.GetCityRepository(_env.WebRootPath, countryCode);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAvaibleCity(PaginationBase pagination, string code, string name)
        {
            if (string.IsNullOrEmpty(code)) return BadRequest("Codigo de pais invalido");
            ViewBag.CountryCode = code;
            var result = await _services.CityService.GetAllPaginated(pagination, x => x.CountryCode == code && (!string.IsNullOrEmpty(name) ? (x.Name.Contains(name) || x.CountryCode.Contains(name)) : true) );
            return PartialView("_CityPartial", result);
        }

        [HttpPost]
        public async Task<IActionResult> AddCity([FromBody]AvaibleCity model)
        {
            var result = await _services.CityService.Add(model);
            if (!result) return BadRequest("Error intente de nuevo mas tarde");
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveCity(int id)
        {
            var result = await _services.CityService.Remove(id);
            if (!result) return BadRequest("Intente de nuevo mas tarde");
            return Ok(result);
        }

        #endregion
    }
}
