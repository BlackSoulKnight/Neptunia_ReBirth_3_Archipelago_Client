using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep3ArchipelagoClient.Neptunia_3_Data
{
    public class Events:Data.Events
    {
        public Events()
        {
            PermanentEvents.AddRange([0,1,2,3,4,18,19,20,21,22,23,1014,1015,3014,3013,3332,8992]);
            UnlockableEvents.AddRange([1013]);
        }
    }
}
