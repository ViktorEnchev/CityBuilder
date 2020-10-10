using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CityBuilder.Data;
using CityBuilder.Data.Entities;
using CityBuilder.Models.CustomeExceptions;
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
                throw new NotFoundException($"Road with id: {id} doesn't exist");
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
                throw new BadRequestException($"Road with name: {roadInputModel.RoadName.ToLower()} already exists");
            }

            if (!this.context.Cities.Any(c => c.Id == roadInputModel.FirstCityId))
            {
                throw new NotFoundException($"City with id: {roadInputModel.FirstCityId} doesn't exist");
            }

            if (!this.context.Cities.Any(c => c.Id == roadInputModel.SecondCityId))
            {
                throw new NotFoundException($"City with id: {roadInputModel.SecondCityId} doesn't exist");
            }

            if(roadInputModel.FirstCityId == roadInputModel.SecondCityId)
            {
                throw new BadRequestException($"First and Second city must not be the same");
            }

            if (roadInputModel.RoadLength < 1)
            {
                throw new BadRequestException($"Road length must be a positive number");
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
                throw new NotFoundException($"Road with id: {id} doesn't exist");
            }

            var road = this.context.Roads.FirstOrDefault(r => r.Id == id);

            this.context.Roads.Remove(road);
            this.context.SaveChanges();

            return this.GetRoadsWithCities();
        }
    }
}
