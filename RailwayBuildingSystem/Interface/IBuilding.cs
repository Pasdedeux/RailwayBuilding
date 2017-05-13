using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailwayBuildingSystem.Interface
{
    interface IBuilding
    {
        string BuildingMajors { get; set; }
        string BuildingType { get; set; }
        string BuildingName { get; set; }
        double? Area { get; set; }
        double? Height { get; set; }
        double? Location { get; set; }
        int? FireLevel { get; set; }
    }
}
