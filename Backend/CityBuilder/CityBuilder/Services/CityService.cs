using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CityBuilder.Data;
using CityBuilder.Data.Entities;
using CityBuilder.Models.CustomeExceptions;
using CityBuilder.Models.DTOs.RoadDTOs;
using CityBuilder.Models.InputModels.CityInputModels;
using CityBuilder.Models.OutputModels.CityOutputModels;
using CityBuilder.Models.OutputModels.RoadOutputModels;
using Microsoft.EntityFrameworkCore;

namespace CityBuilder.Services
{
    public class CityService
    {
        public CityService(CityBuilderDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        private readonly CityBuilderDbContext context;
        private readonly IMapper mapper;

        public CityRoadsNetworkOutputModel GetCityRoadsNetwork(int id)
        {
            if (!this.context.Cities.Any(c => c.Id == id))
            {
                throw new NotFoundException($"City with id: {id} doesn't exist");
            }

            var city = this.context.Cities.FirstOrDefault(c => c.Id == id);
            var cityRoadsNetwork = this.mapper.Map<CityRoadsNetworkOutputModel>(city);

            var roads = this.context.Roads.Include(r => r.FirstCity).Include(r => r.SecondCity).Where(r => r.FirstCityId == id || r.SecondCityId == id)
                .ToList();

            cityRoadsNetwork.Roads = roads.Select(r => new RoadToSecondCity()
            {
                Id = r.Id,

                RoadName = r.RoadName,

                RoadLength = r.RoadLength,

                SecondCityId = r.FirstCityId == id ? r.SecondCityId : r.FirstCityId,

                SecondCity = r.FirstCityId == id
                    ? this.mapper.Map<CityOutputModel>(r.SecondCity)
                    : this.mapper.Map<CityOutputModel>(r.FirstCity),

                RoadCreatedTime = r.RoadCreatedTime

            }).ToList();

            return cityRoadsNetwork;
        }

        public CitiesOutputModel GetCities()
        {
            var cities = this.context.Cities.ToList();

            var resultCities = this.mapper.Map<CitiesOutputModel>(cities);

            return resultCities;
        }

        public async Task<CityOutputModel> AddCity(AddCityInputModel city)
        {
            if (this.context.Cities.Any(c => c.Name.ToLower() == city.Name.ToLower()))
            {
                throw new BadRequestException($"City with name: {city.Name.ToLower()} already exists");
            };

            if (city.Population < 1)
            {
                throw new BadRequestException($"City population must be a positive number");
            };

            var newCity = this.mapper.Map<City>(city);
            newCity.CityCreatedTime = DateTime.Now;

            var result = await this.context.Cities.AddAsync(newCity);
            this.context.SaveChanges();

            var resultCity = this.mapper.Map<CityOutputModel>(result.Entity);

            return resultCity;
        }

        public CitiesRoadsOutputModel DeleteCity(int id)
        {
            if (!this.context.Cities.Any(c => c.Id == id))
            {
                throw new NotFoundException($"City with id: {id} doesn't exist");
            }

            var city = this.context.Cities.FirstOrDefault(c => c.Id == id);
            var roads = this.context.Roads.Include(r => r.FirstCity).Include(r => r.SecondCity).Where(r => r.FirstCityId == id || r.SecondCityId == id).ToList();

            this.context.Cities.Remove(city);
            this.context.Roads.RemoveRange(roads);
            this.context.SaveChanges();

            var citiest = this.mapper.Map<ICollection<CityOutputModel>>(this.context.Cities.ToList());
            var roadsList = this.mapper.Map<ICollection<RoadOutputModel>>(this.context.Roads.ToList());

            var result = new CitiesRoadsOutputModel()
            {
                Cities = citiest,
                Roads = roadsList
            };

            return result;
        }
    }
}
