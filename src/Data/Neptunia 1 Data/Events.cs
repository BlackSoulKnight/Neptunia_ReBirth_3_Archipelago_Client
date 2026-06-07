using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep3ArchipelagoClient.Data.Neptunia_1_Data
{
    public class Events:Data.Events
    {

        public Events()
        {
            PermanentEvents.AddRange([1,2,3,101,102]);
            UnlockableEvents.AddRange([0]);
        }
    }
}
