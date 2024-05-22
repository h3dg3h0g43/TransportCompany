using System.Collections.Generic;

namespace TransComp.Data.Models
{

    public class Truck
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Power { get; set; }
        public string Chassis { get; set; }
        public int Mileage { get; set; }
        public string TruckImagePath { get; set; }
        public int DepotId { get; set; }
        public List<TruckPhoto> TruckPhotos { get; set; }
        public List<Driver> Drivers { get; set; }
        public List<Route> Routes { get; set; }
    }
}