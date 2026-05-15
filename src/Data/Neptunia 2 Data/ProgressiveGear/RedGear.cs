using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep3ArchipelagoClient.Neptunia_2_Data.ProgressiveGear
{
    internal class RedGear : Neptunia_Data.ProgressiveGear
    {
        public RedGear()
        {
            GearList.Add(0, new List<short>() { 1258 });
            GearList.Add(1, new List<short>() { 1265 });
            GearList.Add(2, new List<short>() { 1268 });
            GearList.Add(3, new List<short>() { 1269 });
            GearList.Add(4, new List<short>() { 1270 });
        }
    }
}
