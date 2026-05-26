using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep3ArchipelagoClient.Neptunia_1_Data.ProgressiveGear
{
    internal class IFGear : Neptunia_Data.ProgressiveGear
    {
        public IFGear()
        {
            GearList.Add(0, new List<short>() { 1242 });
            GearList.Add(1, new List<short>() { 1252 });
            GearList.Add(2, new List<short>() { 1255 });
            GearList.Add(3, new List<short>() { 1256 });
            GearList.Add(4, new List<short>() { 1257 });
        }
    }
}