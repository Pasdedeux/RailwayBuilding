using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailwayBuildingSystem.Interface
{
    interface IHVAC
    {
        int AirCondition { get; set; }
        int Wind { get; set; }
        int Firehydrant { get; set; }
        int GasFirehydrant { get; set; }
        int WaterFirehydrant { get; set; }
        int FireCannon { get; set; }
        int Extinguisher { get; set; }
    }
}
