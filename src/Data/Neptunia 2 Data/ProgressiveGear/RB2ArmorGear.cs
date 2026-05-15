using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep3ArchipelagoClient.Neptunia_2_Data.ProgressiveGear
{
    internal class RB2ArmorGear : Neptunia_Data.ProgressiveGear
    {
        public RB2ArmorGear()
        {
            GearList.Add(0, new List<short>() { 1627, 1628 });
            GearList.Add(1, new List<short>() { 1634, 1635 });
            GearList.Add(2, new List<short>() { 1638, 1639, 1640 });
            GearList.Add(3, new List<short>() { 1644, 1645, 1643 });
            GearList.Add(4, new List<short>() { 1646, 1647, 1648, 1649, 1650, 1651, 1652, 1653 });
            GearList.Add(5, new List<short>() { 1654, 1655, 1656, 1567 });
            Amount = 20;
        }
    }
}
