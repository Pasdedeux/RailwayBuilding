using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailwayBuildingSystem.Interface
{
    interface IUser
    {
        uint ID { get; set; }
        string Author { get; set; }
        int Time { get; set; }
    }
}
