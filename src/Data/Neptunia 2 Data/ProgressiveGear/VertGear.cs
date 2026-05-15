using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep3ArchipelagoClient.Neptunia_2_Data.ProgressiveGear
{
    internal class VertGear : Neptunia_Data.ProgressiveGear
    {
        public VertGear()
        {
            GearList.Add(0, new List<short>() { 1207 });
            GearList.Add(1, new List<short>() { 1209 });
            GearList.Add(2, new List<short>() { 1213 });
            GearList.Add(3, new List<short>() { 1210 });
            GearList.Add(4, new List<short>() { 1215 });
            GearList.Add(5, new List<short>() { 1217 });
        }
    }
}
