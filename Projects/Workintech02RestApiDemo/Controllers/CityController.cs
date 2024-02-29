using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Workintech02RestApiDemo.Business.City;
using Workintech02RestApiDemo.Domain.Dto;
using Workintech02RestApiDemo.Domain.Entities;

namespace Workintech02RestApiDemo.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        //[Authorize(Roles="admin,employee")]
        [HttpGet]
        public async  Task<IActionResult> GetAll()
        {
            var cities =await _cityService.GetCities();
            Log.Logger.Information("Cities are fetched. @{cities}",cities);
            return Ok(cities);
        }

        //[Authorize]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var city = _cityService.GetById(id);
            if (city == null)
            {
                Log.Logger.Warning("City is not found. @{id}",id);
                return NotFound();
            }
            Log.Logger.Information("City is fetched. @{city}",city);
            return Ok(city);
        }

        [HttpPost]
        public CityDto AddCity(string name)
        {
            var addedCity = _cityService.AddCity(name);
            Log.Logger.Information("City is added. @{city}",name);
            return addedCity;
        }

        [HttpPut("{id}")]
        public CityDto UpdateCity(int id, string name)
        {
            var updatedCity = _cityService.UpdateCity(id, name);
            Log.Logger.Information("City is updated. @{city}",updatedCity);
            return updatedCity;
        }

        [HttpDelete("{id}")]
        public void DeleteCity(int id)
        {
            _cityService.DeleteCity(id);
            Log.Logger.Information("City is deleted. @{id}",id);
        }
    }
}
