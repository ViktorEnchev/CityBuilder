using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CityBuilder.Data;
using CityBuilder.Data.Entities;
using CityBuilder.Models.InputModels.RoadInputModels;
using CityBuilder.Models.OutputModels.RoadOutputModels;
using Microsoft.EntityFrameworkCore;

namespace CityBuilder.Services
{
    public class RoadService
    {
        public RoadService(CityBuilderDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        private readonly CityBuilderDbContext context;
        private readonly IMapper mapper;

        public RoadOutputModel GetRoadWithCities(int id)
        {
            if (!this.context.Roads.Any(r => r.Id == id))
            {
                // TODO: Add error handling for Road with id doesn't exists
                return null;
            }

            var road = this.context.Roads.Include(r => r.FirstCity).Include(r => r.SecondCity).FirstOrDefault(r => r.Id == id);

            var resultRoad = this.mapper.Map<RoadOutputModel>(road);

            return resultRoad;
        }

        public RoadsOutputModel GetRoadsWithCities()
        {
            var roads = this.context.Roads.Include(r => r.FirstCity).Include(r => r.SecondCity).ToList();

            var resultRoads = this.mapper.Map<RoadsOutputModel>(roads);

            return resultRoads;
        }

        public async Task<RoadOutputModel> AddRoadBetweenCities(AddRoadInputModel roadInputModel)
        {
            if(this.context.Roads.Any(r => r.RoadName.ToLower() == roadInputModel.RoadName.ToLower()))
            {
                // TODO: Add error handling for duplicate Roads
                return null;
            }

            if (!this.context.Cities.Any(c => c.Id == roadInputModel.FirstCityId))
            {
                // TODO: Add error handling for First City doesn't exist
                return null;
            }

            if (!this.context.Cities.Any(c => c.Id == roadInputModel.SecondCityId))
            {
                // TODO: Add error handling for Second City doesn't exist
                return null;
            }

            if(roadInputModel.FirstCityId == roadInputModel.SecondCityId)
            {
                // TODO: Add error handling for Both cities are the same
                return null;
            }

            if (roadInputModel.Population < 1)
            {
                // TODO: Add error handling Population 0 or below 0
                return null;
            }

            var newRoad = this.mapper.Map<Road>(roadInputModel);
            newRoad.RoadCreatedTime = DateTime.Now;

            var resultRoad = await this.context.Roads.AddAsync(newRoad);
            this.context.SaveChanges();

            return this.GetRoadWithCities(resultRoad.Entity.Id);
        }

        public RoadsOutputModel DeleteRoad(int id)
        {
            if(!this.context.Roads.Any(r => r.Id == id))
            {
                // TODO: Add error handling for road with id doesn't exist
                return null;
            }

            var road = this.context.Roads.FirstOrDefault(r => r.Id == id);

            this.context.Roads.Remove(road);
            this.context.SaveChanges();

            return this.GetRoadsWithCities();
        }
    }
}
