using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep3ArchipelagoClient.Neptunia_2_Data.ProgressiveGear
{
    internal class MarvyGear : Neptunia_Data.ProgressiveGear
    {
        public MarvyGear()
        {
            GearList.Add(0, new List<short>() { 1333 });
            GearList.Add(1, new List<short>() { 1335 });
            GearList.Add(2, new List<short>() { 1337 });
            GearList.Add(3, new List<short>() { 1341 });
            GearList.Add(4, new List<short>() { 1342 });
        }
    }
}
