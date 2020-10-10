using System;
using System.Collections.Generic;
using AutoMapper;
using CityBuilder.Data.Entities;
using CityBuilder.Models.InputModels.CityInputModels;
using CityBuilder.Models.OutputModels.CityOutputModels;

namespace CityBuilder.AutoMapper
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {

            this.CreateMap<AddCityInputModel, City>();

            this.CreateMap<City, CityOutputModel>();
            
            this.CreateMap<ICollection<City>, CitiesOutputModel>()
                .ForMember(d => d.Cities, opt => opt.MapFrom(s => s));

            this.CreateMap<City, CityRoadsNetworkOutputModel>();

        }
    }
}
