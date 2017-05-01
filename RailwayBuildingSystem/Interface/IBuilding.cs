using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailwayBuildingSystem.Interface
{
    interface IBuilding
    {
        string BuildingMajoys { get; set; }
        string BuildingType { get; set; }
        string BuildingName { get; set; }
        float Area { get; set; }
        float Height { get; set; }
        float Location { get; set; }
        int FireLevel { get; set; }
    }
}
