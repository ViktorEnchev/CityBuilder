using System;

namespace CityBuilder.Models.InputModels.RoadInputModels
{
    public class AddRoadInputModel
    {

        public string RoadName { get; set; }

        public long Population { get; set; }

        public int FirstCityId { get; set; }

        public int SecondCityId { get; set; }

    }
}