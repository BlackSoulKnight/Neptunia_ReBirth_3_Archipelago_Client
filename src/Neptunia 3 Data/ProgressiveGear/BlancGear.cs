using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep3ArchipelagoClient.src.Neptunia_3_Data.ProgressiveGear
{
    internal class BlancGear:ProgressiveGear
    {
        public BlancGear()
        {
            GearList.Add(0, new List<short>() { 1175 });
            GearList.Add(1, new List<short>() { 1176,1178,1179,1180,1181,1182,1183});
            GearList.Add(2, new List<short>() { 1184,1185,1186,1187,1188});
            GearList.Add(3, new List<short>() { 1189, 1190 });
            GearList.Add(4, new List<short>() { 1191, 1192 });
            GearList.Add(5, new List<short>() { 1193, 1194 });
        }
    }
}
