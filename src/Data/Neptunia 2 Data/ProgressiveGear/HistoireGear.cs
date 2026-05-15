using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep3ArchipelagoClient.Neptunia_2_Data.ProgressiveGear
{
    internal class HistoireGear : ProgressiveGear
    {
        public HistoireGear()
        {
            GearList.Add(0, new List<short>() { 1381 });
            GearList.Add(1, new List<short>() { 1382 });
            GearList.Add(2, new List<short>() { 1384 });
            GearList.Add(3, new List<short>() { 1286 });
        }
    }
}
