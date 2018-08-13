using SQLite;

namespace MapAirportRoute
{
    public class AirPorts
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string IATA { get; set; }

        public string Latitute { get; set; }

        public string Longitude { get; set; }
    }
}
