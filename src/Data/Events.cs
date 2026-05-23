using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep3ArchipelagoClient.Data
{
    public abstract class Events
    {
        protected List<short> PermanentEvents = new();
        protected List<short> UnlockableEvents = new();
        public List<short> UnlockedEvents = new();
        protected Events() { }
        public bool IsEventAvailable(short id)
        {
            if (PermanentEvents.Contains(id))
                return true;
            if (UnlockedEvents.Contains(id))
                return true;
            return false;
        }
        public short[] GetUnlockableEvents => UnlockableEvents.ToArray();
    }
}
