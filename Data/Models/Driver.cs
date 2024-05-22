using System.Collections.Generic;

namespace TransComp.Data.Models
{

    public class Driver
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Phone { get; set; }
        public int Mileage { get; set; }
        public string DriverImagePath { get; set; }
        public int TruckId { get; set; }
        public List<DriverPhoto> DriverPhotos { get; set; }
    }
}