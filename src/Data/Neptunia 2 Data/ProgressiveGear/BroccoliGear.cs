using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep3ArchipelagoClient.Neptunia_2_Data.ProgressiveGear
{
    internal class BroccoliGear : ProgressiveGear
    {
        public BroccoliGear()
        {
            GearList.Add(0, new List<short>() { 1271 });
            GearList.Add(1, new List<short>() { 1275 });
            GearList.Add(2, new List<short>() { 1278 });
            GearList.Add(3, new List<short>() { 1279 });
            GearList.Add(4, new List<short>() { 1280 });
        }
    }
}
