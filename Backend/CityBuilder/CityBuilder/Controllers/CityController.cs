using System.Threading.Tasks;
using CityBuilder.Models.InputModels.CityInputModels;
using CityBuilder.Models.OutputModels.CityOutputModels;
using CityBuilder.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CityBuilder.Controllers
{
    [Authorize]
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
        public async Task<ActionResult<CityRoadsNetworkOutputModel>> GetCityRoadsNetwork(int id)
        {
            var city = await this.cityService.GetCityRoadsNetwork(id);
            return Ok(city);
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<CitiesOutputModel>> GetCities()
        {
            var cities = await this.cityService.GetCities();
            return Ok(cities);
        }

        [HttpPost]
        public async Task<ActionResult<CityOutputModel>> AddCity(AddCityInputModel cityInputModel)
        {
            var newCity = await this.cityService.AddCity(cityInputModel);
            return Ok(newCity);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<CitiesRoadsOutputModel>> DeleteCity(int id)
        {
            var cities = await this.cityService.DeleteCity(id);
            return Ok(cities);
        }
    }
}
