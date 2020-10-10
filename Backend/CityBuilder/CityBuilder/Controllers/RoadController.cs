using System;
using System.Threading.Tasks;
using CityBuilder.Data.Entities;
using CityBuilder.Models.InputModels;
using CityBuilder.Models.InputModels.RoadInputModels;
using CityBuilder.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CityBuilder.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class RoadController : ControllerBase
    {
        public RoadController(RoadService roadService)
        {
            this.roadService = roadService;
        }

        private readonly RoadService roadService;

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetRoadWithCities(int id)
        {
            var newCity = this.roadService.GetRoadWithCities(id);
            return Ok(newCity);
        }

        [HttpGet]
        [Route("all")]
        public IActionResult GetRoadsWithCities()
        {
            var roads = this.roadService.GetRoadsWithCities();
            return Ok(roads);
        }

        [HttpPost]
        public async Task<IActionResult> AddRoadBetweenCities(AddRoadInputModel roadInputModel)
        {
            var newRoad = await this.roadService.AddRoadBetweenCities(roadInputModel);
            return Ok(newRoad);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteRoad(int id)
        {
            var roads = this.roadService.DeleteRoad(id);
            return Ok(roads);
        }
    }
}
