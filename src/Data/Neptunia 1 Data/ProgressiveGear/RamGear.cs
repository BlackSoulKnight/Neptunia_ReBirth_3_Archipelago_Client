using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep3ArchipelagoClient.Neptunia_1_Data.ProgressiveGear
{
    internal class RamGear : Neptunia_Data.ProgressiveGear
    {
        public RamGear()
        {
            GearList.Add(0, new List<short>() { 1161 });
            GearList.Add(1, new List<short>() { 1166 });
            GearList.Add(2, new List<short>() { 1175 });
            GearList.Add(3, new List<short>() { 1176 });
            GearList.Add(4, new List<short>() { 1176 });
            GearList.Add(5, new List<short>() { 1177 });
            GearList.Add(6, new List<short>() { 1178 });
        }
    }
}
