using SQLite;

namespace MapAirportRoute
{
    public class Receipe
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string Name { get; set; }

    }
}
