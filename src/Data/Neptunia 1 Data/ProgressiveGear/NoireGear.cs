using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep3ArchipelagoClient.Neptunia_1_Data.ProgressiveGear
{
    internal class NoireGear : Neptunia_Data.ProgressiveGear
    {
        public NoireGear()
        {
            GearList.Add(0, new List<short>() { 1196 });
            GearList.Add(1, new List<short>() { 1197 });
            GearList.Add(2, new List<short>() { 1201 });
            GearList.Add(3, new List<short>() { 1205 });
            GearList.Add(4, new List<short>() { 1202 });
            GearList.Add(5, new List<short>() { 1204 });
        }
    }
}
