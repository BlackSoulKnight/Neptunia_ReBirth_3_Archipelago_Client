using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep3ArchipelagoClient.src.Neptunia_3_Data.ProgressiveGear
{
    internal class VertGear:ProgressiveGear
    {
        public VertGear()
        {
            GearList.Add(0, new List<short>() { 1197 });
            GearList.Add(1, new List<short>() { 1198,1199,1200,1201,1202});
            GearList.Add(2, new List<short>() { 1203,1204,1206,1207});
            GearList.Add(3, new List<short>() { 1208, 1209 });
            GearList.Add(4, new List<short>() { 1210, 1211 });
            GearList.Add(5, new List<short>() { 1212, 1213 });
        }
    }
}
