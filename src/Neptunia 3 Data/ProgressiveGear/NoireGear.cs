using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep3ArchipelagoClient.src.Neptunia_3_Data.ProgressiveGear
{
    internal class NoireGear:ProgressiveGear
    {
        public NoireGear()
        {
            GearList.Add(0, new List<short>() { 1151 });
            GearList.Add(1, new List<short>() { 1152,1153,1154,1155,1156,1157,1158,1159});
            GearList.Add(2, new List<short>() { 1160,1161,1162,1163,1164});
            GearList.Add(3, new List<short>() { 1165,1166,1167,1168});
            GearList.Add(4, new List<short>() { 1169 });
            GearList.Add(5, new List<short>() { 1170, 1172 });
            GearList.Add(6, new List<short>() { 1171 });
        }
    }
}
