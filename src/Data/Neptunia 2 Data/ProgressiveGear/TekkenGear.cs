using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep3ArchipelagoClient.Neptunia_2_Data.ProgressiveGear
{
    internal class TekkenGear : Neptunia_Data.ProgressiveGear
    {
        public TekkenGear()
        {
            GearList.Add(0, new List<short>() { 1343 });
            GearList.Add(1, new List<short>() { 1344 });
            GearList.Add(2, new List<short>() { 1347 });
            GearList.Add(3, new List<short>() { 1351 });
            GearList.Add(4, new List<short>() { 1352 });
        }
    }
}
