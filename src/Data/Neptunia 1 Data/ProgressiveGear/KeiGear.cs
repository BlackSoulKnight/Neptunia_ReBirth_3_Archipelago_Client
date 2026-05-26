using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep3ArchipelagoClient.Neptunia_1_Data.ProgressiveGear
{
    internal class KeiGear : Neptunia_Data.ProgressiveGear
    {
        public KeiGear()
        {
            GearList.Add(0, new List<short>() { 1363 });
            GearList.Add(1, new List<short>() { 1365 });
            GearList.Add(2, new List<short>() { 1367 });
            GearList.Add(3, new List<short>() { 1368 });
        }
    }
}
