using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RailwayBuildingSystem.Tool
{
    using RailwayBuildingSystem.Interface;
    class DataProxy : IUser, IBuilding, IHVAC
    {
        public int? AirConditioning { get; set; }

        public double Area { get; set; }

        public string Author { get; set; }

        public string BuildingMajors { get; set; }

        public string BuildingName { get; set; }

        public string BuildingType { get; set; }

        public int? Extinguisher { get; set; }

        public int? FireCannon { get; set; }

        public int? Firehydrant { get; set; }

        public int? FireLevel { get; set; }

        public int? GasFirehydrant { get; set; }

        public double Height { get; set; }

        public int ID { get; set; }

        public double Location { get; set; }

        public int Time { get; set; }

        public int? WaterFirehydrant { get; set; }

        public int? Wind { get; set; }
    }
}
