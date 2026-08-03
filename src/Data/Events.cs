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
        protected List<short> UnlockedEvents = new();
        protected Dictionary<short,List<short>> PrequisiteEvents = new();

        public void AddEvent(short eventID)
        {
            Mod.APClient.SaveEvent(eventID);
            UnlockedEvents.Add(eventID);
        }
        public void AddEvent(short[] eventID)
        {
            foreach(var evnt in eventID)
                AddEvent(evnt);
        }

        public bool EventUnlocked(short eventID) => UnlockedEvents.Contains(eventID) || PermanentEvents.Contains(eventID);
        protected Events() { }
        public bool IsEventAvailable(short id)
        {
            if(PrequisiteEvents.ContainsKey(id))
                foreach(short eventID in PrequisiteEvents[id])
                    if(!Mod.SaveGame.IsEventFlagSet(eventID))
                        return false;
            if (PermanentEvents.Contains(id))
                return true;
            if (UnlockedEvents.Contains(id))
                return true;
            return false;
        }
        public short[] GetUnlockableEvents => UnlockableEvents.ToArray();
        protected void AddPrequisiteEvent(short eventID,short prequisiteEventID)
        {
            if (!PrequisiteEvents.ContainsKey(eventID))
                PrequisiteEvents[eventID] = new List<short>();
            PrequisiteEvents[eventID].Add(prequisiteEventID);
        }
        protected void AddPrequisiteEvent(short eventID,short[] prequisiteEventID)
        {
            if (!PrequisiteEvents.ContainsKey(eventID))
                PrequisiteEvents[eventID] = new List<short>();
            PrequisiteEvents[eventID].AddRange(prequisiteEventID);
        }
    }
}
