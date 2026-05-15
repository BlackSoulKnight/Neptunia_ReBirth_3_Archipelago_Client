using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep3ArchipelagoClient.Neptunia_2_Data.ProgressiveGear
{
    internal class FalcomGear : Neptunia_Data.ProgressiveGear
    {
        public FalcomGear()
        {
            GearList.Add(0, new List<short>() { 1281 });
            GearList.Add(1, new List<short>() { 1286 });
            GearList.Add(2, new List<short>() { 1292 });
            GearList.Add(3, new List<short>() { 1294 });
            GearList.Add(4, new List<short>() { 1296 });
        }
    }
}
