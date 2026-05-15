using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep3ArchipelagoClient.Neptunia_2_Data.ProgressiveGear
{
    internal class ChikaGear : Neptunia_Data.ProgressiveGear
    {
        public ChikaGear()
        {
            GearList.Add(0, new List<short>() { 1375 });
            GearList.Add(1, new List<short>() { 1377 });
            GearList.Add(2, new List<short>() { 1379 });
            GearList.Add(3, new List<short>() { 1380 });
        }
    }
}
