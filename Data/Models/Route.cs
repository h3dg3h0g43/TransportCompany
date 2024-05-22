using System;

namespace TransComp.Data.Models
{

    public class Route
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndTime { get; set; }
        public int TruckId { get; set; }
    }
}