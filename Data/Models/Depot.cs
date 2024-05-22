using System.Collections.Generic;

namespace TransportComp.Data.Models
{

    public class Depot
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public List<Truck> Trucks { get; set; }
    }
}