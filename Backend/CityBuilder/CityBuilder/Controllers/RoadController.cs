using System;
using System.Threading.Tasks;
using CityBuilder.Data.Entities;
using CityBuilder.Models.InputModels;
using CityBuilder.Models.InputModels.RoadInputModels;
using CityBuilder.Models.OutputModels.RoadOutputModels;
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
        public async Task<ActionResult<RoadOutputModel>> GetRoadWithCities(int id)
        {
            var road = await this.roadService.GetRoadWithCities(id);
            return Ok(road);
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<RoadsOutputModel>> GetRoadsWithCities()
        {
            var roads = await this.roadService.GetRoadsWithCities();
            return Ok(roads);
        }

        [HttpPost]
        public async Task<ActionResult<RoadOutputModel>> AddRoadBetweenCities(AddRoadInputModel roadInputModel)
        {
            var newRoad = await this.roadService.AddRoadBetweenCities(roadInputModel);
            return Ok(newRoad);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<RoadsOutputModel>> DeleteRoad(int id)
        {
            var roads = await this.roadService.DeleteRoad(id);
            return Ok(roads);
        }
    }
}
