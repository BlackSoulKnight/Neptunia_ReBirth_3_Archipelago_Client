using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep3ArchipelagoClient.Neptunia_3_Data.ProgressiveGear
{
    internal class UniGear: Neptunia_Data.ProgressiveGear
    {
        public UniGear()
        {
            GearList.Add(0, new List<short>() { 1245 });
            GearList.Add(1, new List<short>() { 1247 });
            GearList.Add(2, new List<short>() { 1248 });
            GearList.Add(3, new List<short>() { 1250 });
        }
    }
}
