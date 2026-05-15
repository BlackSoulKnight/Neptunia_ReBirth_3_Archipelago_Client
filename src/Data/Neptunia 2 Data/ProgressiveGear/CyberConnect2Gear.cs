using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep3ArchipelagoClient.Neptunia_2_Data.ProgressiveGear
{
    internal class CyberConnect2Gear : ProgressiveGear
    {
        public CyberConnect2Gear()
        {
            GearList.Add(0, new List<short>() { 1323 });
            GearList.Add(1, new List<short>() { 1325 });
            GearList.Add(2, new List<short>() { 1327 });
            GearList.Add(3, new List<short>() { 1331 });
            GearList.Add(4, new List<short>() { 1332 });
        }
    }
}
