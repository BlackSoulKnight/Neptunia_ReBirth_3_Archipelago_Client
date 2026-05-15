using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep3ArchipelagoClient.Neptunia_2_Data.ProgressiveGear
{
    internal class MinaGear : ProgressiveGear
    {
        public MinaGear()
        {
            GearList.Add(0, new List<short>() { 1369 });
            GearList.Add(1, new List<short>() { 1371 });
            GearList.Add(2, new List<short>() { 1373 });
            GearList.Add(3, new List<short>() { 1374 });
        }
    }
}
