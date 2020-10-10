using System.Threading.Tasks;
using CityBuilder.Models.InputModels.CityInputModels;
using CityBuilder.Services;
using Microsoft.AspNetCore.Mvc;

namespace CityBuilder.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CityController : ControllerBase
    {
        public CityController(CityService cityService)
        {
            this.cityService = cityService;
        }

        private readonly CityService cityService;

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetCityRoadsNetwork(int id)
        {
            var newCity = this.cityService.GetCityRoadsNetwork(id);
            return Ok(newCity);
        }

        [HttpPost]
        [Route("all")]
        public IActionResult GetCities()
        {
            var cities = this.cityService.GetCities();
            return Ok(cities);
        }

        [HttpPost]
        public async Task<IActionResult> AddCity(AddCityInputModel city)
        {
            var newCity = await this.cityService.AddCity(city);
            return Ok(newCity);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteCity(int id)
        {
            var cities = this.cityService.DeleteCity(id);
            return Ok(cities);
        }
    }
}
