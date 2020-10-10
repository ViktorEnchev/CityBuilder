using System;
namespace CityBuilder.Models.OutputModels.CityOutputModels
{
    public class CityOutputModel
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CityCreatedTime { get; set; }

        public long Population { get; set; }

    }
}
