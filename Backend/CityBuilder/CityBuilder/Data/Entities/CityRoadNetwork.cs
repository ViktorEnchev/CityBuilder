namespace CityBuilder.Data.Entities
{
    public class CityRoadNetwork
    {
        public int Id { get; set; }

        public int CityId { get; set; }

        public City City { get; set; }

        public int RoadId { get; set; }

        public Road Road { get; set; }
    }
}
