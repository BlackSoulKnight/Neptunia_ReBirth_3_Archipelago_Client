using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep3ArchipelagoClient.Neptunia_1_Data.ProgressiveGear
{
    internal class NepgearGear : Neptunia_Data.ProgressiveGear
    {
        public NepgearGear()
        {
            GearList.Add(0, new List<short>() { 1101 });
            GearList.Add(1, new List<short>() { 1112 });
            GearList.Add(2, new List<short>() { 1115 });
            GearList.Add(3, new List<short>() { 1119 });
            GearList.Add(4, new List<short>() { 1118 });
            GearList.Add(5, new List<short>() { 1117 });
            GearList.Add(6, new List<short>() { 1120 });
        }
    }
}
