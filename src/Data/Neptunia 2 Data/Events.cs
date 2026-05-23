using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep3ArchipelagoClient.Data.Neptunia_2_Data
{
    public static class Events
    {
        public static HashSet<int> AllowedEvents = new() {0,1,2,3,4,11,12,51,52,53,
            513,516,519, //cpu fights
            524,525,526
        };
    }
}
