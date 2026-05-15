using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep3ArchipelagoClient.Neptunia_2_Data.ProgressiveGear
{
    internal class UniGear : ProgressiveGear
    {
        public UniGear()
        {
            GearList.Add(0, new List<short>() { 1123 });
            GearList.Add(1, new List<short>() { 1128 });
            GearList.Add(2, new List<short>() { 1133 });
            GearList.Add(3, new List<short>() { 1139 });
            GearList.Add(4, new List<short>() { 1129 });
            GearList.Add(5, new List<short>() { 1138 });
            GearList.Add(6, new List<short>() { 1140 });
        }
    }
}
