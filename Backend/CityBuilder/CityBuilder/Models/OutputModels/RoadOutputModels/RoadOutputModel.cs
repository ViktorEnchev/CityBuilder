using System;
using CityBuilder.Data.Entities;

namespace CityBuilder.Models.OutputModels.RoadOutputModels
{
    public class RoadOutputModel
    {

        public int Id { get; set; }

        public string RoadName { get; set; }

        public long RoadLength { get; set; }

        public int FirstCityId { get; set; }

        public virtual City FirstCity { get; set; }

        public int SecondCityId { get; set; }

        public virtual City SecondCity { get; set; }

        public DateTime RoadCreatedTime { get; set; }

    }
}
