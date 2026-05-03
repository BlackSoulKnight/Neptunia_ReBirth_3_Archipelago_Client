using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep3ArchipelagoClient.src.Neptunia_3_Data.ProgressiveGear

{
    internal class NeptuneGear :ProgressiveGear
    {
        public NeptuneGear()
        {
            GearList.Add(0,new List<short>(){1101});
            GearList.Add(1,new List<short>(){1102,1103,1104,1105,1106,1107,1108,1109,1110});
            GearList.Add(2,new List<short>(){1111,1112,1113});
            GearList.Add(3,new List<short>(){1114,1115,1116,1117,1118,1119});
            GearList.Add(4, new List<short>(){1120,1121,1122});
            GearList.Add(5, new List<short>(){1123,1124});
            GearList.Add(6, new List<short>(){1125});
        }

    }
}
