using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep3ArchipelagoClient.Neptunia_2_Data.ProgressiveGear
{
    internal class NeptuneGear : ProgressiveGear
    {
        public NeptuneGear()
        {
            GearList.Add(0, new List<short>() { 1179 });
            GearList.Add(1, new List<short>() { 1183 });
            GearList.Add(2, new List<short>() { 1189 });
            GearList.Add(3, new List<short>() { 1191 });
            GearList.Add(4, new List<short>() { 1192 });
            GearList.Add(5, new List<short>() { 1194, 1195 });
        }
    }
}
