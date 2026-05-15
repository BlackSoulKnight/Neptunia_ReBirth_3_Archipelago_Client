using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep3ArchipelagoClient.Neptunia_2_Data.ProgressiveGear
{
    internal class CaveGear : Neptunia_Data.ProgressiveGear
    {
        public CaveGear()
        {
            GearList.Add(0, new List<short>() { 1297 });
            GearList.Add(1, new List<short>() { 1300, 1301 });
            GearList.Add(2, new List<short>() { 1309, 1310 });
            GearList.Add(3, new List<short>() { 1311 });
            GearList.Add(4, new List<short>() { 1312 });
        }
    }
}
