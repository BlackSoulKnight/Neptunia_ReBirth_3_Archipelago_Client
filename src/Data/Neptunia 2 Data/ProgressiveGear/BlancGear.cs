using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep3ArchipelagoClient.Neptunia_2_Data.ProgressiveGear
{
    internal class BlancGear : Neptunia_Data.ProgressiveGear
    {
        public BlancGear()
        {
            GearList.Add(0, new List<short>() { 1218 });
            GearList.Add(1, new List<short>() { 1219 });
            GearList.Add(2, new List<short>() { 1225 });
            GearList.Add(3, new List<short>() { 1227 });
            GearList.Add(4, new List<short>() { 1224 });
            GearList.Add(5, new List<short>() { 1228 });
        }
    }
}
