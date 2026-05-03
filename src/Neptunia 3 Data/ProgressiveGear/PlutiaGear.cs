using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep3ArchipelagoClient.src.Neptunia_3_Data.ProgressiveGear
{
    internal class PlutiaGear:ProgressiveGear
    {
        public PlutiaGear()
        {
            GearList.Add(0, new List<short>() { 1127 });
            GearList.Add(1, new List<short>() { 1128,1129,1130,1131,1132,1133,1134,1135});
            GearList.Add(2, new List<short>() { 1136,1137,1138,1139,1140,1141,1142});
            GearList.Add(3, new List<short>() { 1143 });
            GearList.Add(4, new List<short>() { 1144, 1145 });
            GearList.Add(5, new List<short>() { 1146, 1147 });
            GearList.Add(6, new List<short>() { 1148 });
        }
    }
}
