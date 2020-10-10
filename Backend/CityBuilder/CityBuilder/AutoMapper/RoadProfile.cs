using System;
using System.Collections.Generic;
using AutoMapper;
using CityBuilder.Data.Entities;
using CityBuilder.Models.InputModels.RoadInputModels;
using CityBuilder.Models.OutputModels.RoadOutputModels;

namespace CityBuilder.AutoMapper
{
    public class RoadProfile : Profile
    {
        public RoadProfile()
        {

            this.CreateMap<AddRoadInputModel, Road>();

            this.CreateMap<Road, RoadOutputModel>();

            this.CreateMap<ICollection<Road>, RoadsOutputModel>()
                .ForMember(dest => dest.Roads, opt => opt.MapFrom(s => s));

        }
    }
}
