using System;
using System.Collections.Generic;

namespace CityBuilder.Data.Entities
{
    public class City
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CityCreatedTime { get; set; }

        public long Population { get; set; }
    }
}
