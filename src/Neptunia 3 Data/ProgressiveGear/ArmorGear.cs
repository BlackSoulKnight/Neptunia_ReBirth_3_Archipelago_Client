using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep3ArchipelagoClient.src.Neptunia_3_Data.ProgressiveGear
{
    internal class ArmorGear:ProgressiveGear
    {
        public ArmorGear()
        {
            GearList.Add(0, new List<short>() { 1601,1602,1603,1604,1605,1606,1607,1608,1609,1610});
            GearList.Add(1, new List<short>() { 1630,1631,1632,1633,1634,1635,1636,1637,1638,1639,1640,1641,1642});
            GearList.Add(2, new List<short>() { 1643,1644,1645,1646,1647,1648,1649,1650,1651,1652,1653,1654});
            GearList.Add(3, new List<short>() { 1655, 1656 });
            GearList.Add(4, new List<short>() { 1657,1658,1659,1660,1661,1662,1663,1664,1665,1666});
            GearList.Add(5, new List<short>() { 1667,1668});
            GearList.Add(6, new List<short>() { 1669, 1670 });
            GearList.Add(7, new List<short>() { 1671});
            Amount = 20;
        }
    }
}
