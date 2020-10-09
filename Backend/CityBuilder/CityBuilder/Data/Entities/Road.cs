using System.Collections.Generic;

namespace CityBuilder.Data.Entities
{
    public class Road
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IList<CityRoadNetwork> CityRoadNetworks { get; set; }
    }
}
