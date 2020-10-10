using System;
using System.Collections.Generic;
using CityBuilder.Models.DTOs.RoadDTOs;

namespace CityBuilder.Models.OutputModels.CityOutputModels
{
    public class CityRoadsNetworkOutputModel
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CityCreatedTime { get; set; }

        public long Population { get; set; }

        public ICollection<RoadToSecondCity> Roads { get; set; }

    }
}
