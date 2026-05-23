using Nep3ArchipelagoClient.Data;
using Nep3ArchipelagoClient.Hooks;
using Nep3ArchipelagoClient.Neptunia_Data;
using Reloaded.Memory;


namespace Nep3ArchipelagoClient
{
    internal abstract class SaveGame
    {
        protected nuint SaveGameOffest;
        public UIntPtr SaveGamePointer => Memory.Instance.Read<uint>(Mod.ModuleBase + SaveGameOffest);
        protected uint APSaveLocation;
        public nuint PlanOffset;
        protected uint EventFlagOffset;
        Memory memory => Memory.Instance;
        public APOptions Options;
        public Events Events;
        protected SaveGame()
        {
        }

        public abstract int CurrentDungeon();

        public bool IsInit => memory.Read<byte>(SaveGamePointer + APSaveLocation - 17) == 1;

        
        public bool IsEventFlagSet(int EventID) => (memory.Read<byte>(SaveGamePointer + EventFlagOffset+(nuint)(EventID/8)) & 1 << (EventID % 8)) > 0;
        public bool AreEventFlagSet(int[] EventID) {
            foreach (int i in EventID)
                if (IsEventFlagSet(i) == false) return false;
            return true;

        }
        public void SetEventFlag(int EventID,bool Active)
        {
            var FlagRegion = memory.Read<byte>(SaveGamePointer + EventFlagOffset + (nuint)(EventID / 8));

            if (Active)
                FlagRegion |= (byte)(1 << (EventID % 8));
            else
                FlagRegion &= (byte)(0xFF - (1 << (EventID % 8)));
            memory.Write<byte>(SaveGamePointer + EventFlagOffset + (nuint)(EventID / 8), FlagRegion);
        }
        protected abstract void DoSetupSaveFile();
        public void SetupSaveFile()
        {
            if (!IsInit)
            {
                Thread.Sleep(100);
                DoSetupSaveFile();
            }
        }

        public abstract void AddDungeon(short dungeonId);
        public abstract void UnlockCity(short cityId);
        public int GetCurrentApItemCount()
        {
            return Memory.Instance.Read<int>(SaveGamePointer + APSaveLocation - 16);
        }
        public void InitGear()
        {
            foreach (var character in ProgressiveGear.ProgressiveGears.Values)
            {
                character.UnlockTier(0);
            }
        }
        public void IncrementCurrentApItemCount()
        {
            var value = Memory.Instance.Read<int>(SaveGamePointer + APSaveLocation - 16) + 1;
            Memory.Instance.Write<int>(SaveGamePointer + APSaveLocation - 16, value);
        }
        public bool GoMode = false;
        protected abstract void _CheckUnlockGoalCondition();
        public void CheckUnlockGoalCondition()
        {
            if (!GoMode)
                _CheckUnlockGoalCondition();
        }
        public abstract bool GoalAchieved(long APLocation);
        
        public unsafe abstract void AddPartyMember(int characterID);

        public abstract void RemovePartyMember(int characterId);

        public abstract void ShowCharacter(int characterId);
    }
}
