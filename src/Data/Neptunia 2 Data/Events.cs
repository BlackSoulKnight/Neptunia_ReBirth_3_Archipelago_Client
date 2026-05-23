using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep3ArchipelagoClient.Data.Neptunia_2_Data
{
    public class Events:Data.Events
    {

        public Events()
        {
            PermanentEvents.AddRange([0,1,2,3,4,11,12,51,52,53]);
            UnlockableEvents.AddRange([513,516,519]);
        }
    }
}
