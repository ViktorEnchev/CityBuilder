using System;
using CityBuilder.Data.Entities;
using CityBuilder.Models.OutputModels.CityOutputModels;

namespace CityBuilder.Models.DTOs.RoadDTOs
{
    public class RoadToSecondCity
    {

        public int Id { get; set; }

        public string RoadName { get; set; }

        public long RoadLength { get; set; }

        public int SecondCityId { get; set; }

        public virtual CityOutputModel SecondCity { get; set; }

        public DateTime RoadCreatedTime { get; set; }

    }
}
