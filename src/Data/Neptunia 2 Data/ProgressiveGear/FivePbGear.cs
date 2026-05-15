using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep3ArchipelagoClient.Neptunia_2_Data.ProgressiveGear
{
    internal class FivePbGear : Neptunia_Data.ProgressiveGear
    {
        public FivePbGear()
        {
            GearList.Add(0, new List<short>() { 1313 });
            GearList.Add(1, new List<short>() { 1316 });
            GearList.Add(2, new List<short>() { 1319 });
            GearList.Add(3, new List<short>() { 1320 });
            GearList.Add(4, new List<short>() { 1322 });
        }
    }
}
