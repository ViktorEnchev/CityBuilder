using System;
using System.Collections.Generic;

namespace CityBuilder.Data.Entities
{
    public class Road
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
