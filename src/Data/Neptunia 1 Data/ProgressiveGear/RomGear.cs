using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep3ArchipelagoClient.Neptunia_1_Data.ProgressiveGear
{
    internal class RomGear : Neptunia_Data.ProgressiveGear
    {
        public RomGear()
        {
            GearList.Add(0, new List<short>() { 1143 });
            GearList.Add(1, new List<short>() { 1148 });
            GearList.Add(2, new List<short>() { 1154 });
            GearList.Add(3, new List<short>() { 1157 });
            GearList.Add(4, new List<short>() { 1158 });
            GearList.Add(5, new List<short>() { 1159 });
            GearList.Add(6, new List<short>() { 1160 });
        }
    }
}
