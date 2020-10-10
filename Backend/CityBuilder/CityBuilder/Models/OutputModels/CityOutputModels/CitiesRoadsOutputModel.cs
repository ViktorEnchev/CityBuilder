using System;
using System.Collections.Generic;
using CityBuilder.Models.OutputModels.RoadOutputModels;

namespace CityBuilder.Models.OutputModels.CityOutputModels
{
    public class CitiesRoadsOutputModel
    {

        public ICollection<CityOutputModel> Cities { get; set; }

        public ICollection<RoadOutputModel> Roads { get; set; }

    }
}
