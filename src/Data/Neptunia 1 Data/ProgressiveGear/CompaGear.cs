using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep3ArchipelagoClient.Neptunia_1_Data.ProgressiveGear
{
    internal class CompaGear : Neptunia_Data.ProgressiveGear
    {
        public CompaGear()
        {
            GearList.Add(0, new List<short>() { 1229 });
            GearList.Add(1, new List<short>() { 1235 });
            GearList.Add(2, new List<short>() { 1236 });
            GearList.Add(3, new List<short>() { 1240 });
            GearList.Add(4, new List<short>() { 1241 });
        }
    }
}
