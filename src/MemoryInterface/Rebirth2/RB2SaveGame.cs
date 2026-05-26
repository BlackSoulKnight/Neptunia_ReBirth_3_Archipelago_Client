using Nep3ArchipelagoClient.Archipelago;
using Nep3ArchipelagoClient.Data.Neptunia_2_Data;
using Nep3ArchipelagoClient.Hooks;
using Nep3ArchipelagoClient.MemoryInterface;
using Nep3ArchipelagoClient.Neptunia_2_Data;
using Reloaded.Memory;


namespace Nep3ArchipelagoClient
{
    internal class RB2SaveGame : SaveGame
    {
        static Memory memory = Memory.Instance;
        Inventory Inventory;
        new RB2Options Options => (RB2Options)base.Options;
        public RB2SaveGame()
        {
            SaveGameOffest = 0x443310;
            Inventory = new RB2Inventory(this);
            APSaveLocation = 0x1032c;
            PlanOffset = 0x443310;
            EventFlagOffset = 0x91c;
            base.Options = new RB2Options();
            Events = new Events();
        }
        public int CurrentItemCount()
        {
            return memory.Read<int>(SaveGamePointer + 0x443310);
        }
        public override int CurrentDungeon()
        {
            return memory.Read<int>(SaveGamePointer - 0x12B6F4);
        }

        protected override void DoSetupSaveFile()
        {

            if (!IsInit && IsEventFlagSet(658))
            {
                //debug stuff
                memory.Write<byte>(SaveGamePointer + APSaveLocation - 17, 1);
                for (int i = 1; i < 10; i++)
                    SetEventFlag(i, false);
                for (int i = 50; i < 55; i++)
                    SetEventFlag(i, false);
                UnlockGameFeatures();
                var startchar = Mod.APClient.GetStartingCharacter();
                AddPartyMember(startchar);
                if (startchar != (int)CharacterId.nepgear)
                    RemovePartyMember((int)CharacterId.nepgear);
                if(startchar!= (int)CharacterId.IF)
                    RemovePartyMember((int)CharacterId.IF);
                if(startchar != (int)CharacterId.compa)
                    RemovePartyMember((int)CharacterId.compa);
                InitGear();

#if DEBUG
                Test_CharacterUnlock();
                Test_DungeonUnlock();
                Test_VGMRun();
                Test_Goal();
                Test_CharacterManip();
                Test_DataStorage();
#endif
            }
        }

        private void Test_DataStorage()
        {
            if (Mod.APClient.IsConnected)
            {
                Mod.APClient.SaveEvent(1);
                Console.WriteLine(Mod.APClient.CheckEvent(1));
                Console.WriteLine(Mod.APClient.CheckEvent(2));

            }
        }

        public override void AddDungeon(short dungeonId)
        {
            nuint dungeonOffset = 0x10330;
            nuint dungeonLenghtOffset = 0x1032c;
            var dungeonListLength = memory.Read<byte>(SaveGamePointer + dungeonLenghtOffset);
            var writeInto = SaveGamePointer + dungeonOffset + (nuint)(0x203c * dungeonListLength);
            memory.Write<byte>(writeInto, 0x0F);
            memory.Write<short>(writeInto + 2, dungeonId);
            memory.Write<byte>(writeInto + 4, (byte)1);
            memory.Write<byte>(SaveGamePointer + dungeonLenghtOffset, ++dungeonListLength);
        }
        public unsafe override void AddPartyMember(int characterID)
        {
            CharacterHooks._addNewCharacter.GetWrapper()((uint)characterID);
            var character = (Character*)CharacterHooks.GetCharacter(characterID);
            if (character == null) return;
            character->Cha = 0x1;
            character->Armor = 1627;
        }
        public override void RemovePartyMember(int characterId) => CharacterHooks._removePartyMember.GetWrapper()(characterId);

        public override void ShowCharacter(int characterId)
        {
            var characterPoint = CharacterHooks._findCharacter.GetWrapper()(characterId);
            if (characterPoint == 0) return;
            var currentVal = memory.Read<byte>(characterPoint);
            currentVal &= 0xff - 0x80;
            memory.Write<byte>(characterPoint, currentVal);
        }
        protected override void _CheckUnlockGoalCondition()
        {
            bool old_sword = Inventory.FindItem(254, out int position);
            if (!old_sword || Inventory.GetItemCountAtSlot(position) < 1) return;
            if (!AreEventFlagSet([513, 516, 519])) return;
            GoMode = true;
            Events.UnlockedEvents.AddRange([524, 525, 526]);
        }

        private void UnlockGameFeatures()
        {

            var flagPionter = SaveGamePointer + 3584;
            for (nuint i = 0; i < 6; i++)
            {
                memory.Write<byte>(flagPionter + i, 0xFF);
                memory.Write<byte>(flagPionter + i + 16, 0xFF);
            }
            for(short id = 2; id <6;id++)
                UnlockCity(id);
            SetEventFlag(172, true);
        }
        void Test_VGMRun()
        {
            Inventory.AddItem(1655, 4);
            Inventory.AddItem(1305, 1);
            Inventory.AddItem(40, 99);

        }
        void Test_DungeonUnlock()
        {
            for(short i = 1; i < 37; i++)
            {
                AddDungeon(i);
            }
        }
        void Test_CharacterUnlock()
        {
            for(short i = 1; i < 26; i++)
            {
                RemovePartyMember(i);
            }
            Thread.Sleep(5000);
            for(short i = 1; i < 26; i++)
            {
                AddPartyMember(i);
            }
        }
        void Test_Goal()
        {
            Events.UnlockedEvents.AddRange(Events.GetUnlockableEvents);
            Inventory.AddItem(254, 1);
        }
        unsafe void Test_CharacterManip()
        {
            var character = (Character*)CharacterHooks.GetCharacter(6);
            character->BaseStr = 800000;
            character->BaseAgi = 800000;
            character->BaseInt = 800000;
            character->BaseMen = 800000;
            character->BaseVit = 800000;
            character->MaxBaseHP = 300000;
            character->BaseTec = 800000;
        }
        public override bool GoalAchieved(long APLocation)
        {
            return APLocation == APClient.EnemyBaseID + 1055;
        }

        public override void UnlockCity(short cityId)
        {
            nuint CitySlotsOffset = 0x10128;
            var cityLength = memory.Read<byte>(SaveGamePointer + CitySlotsOffset);
            var writeInto = SaveGamePointer + CitySlotsOffset + 0x04 + (nuint)(0x8 * cityLength);
            memory.Write<byte>(writeInto, 0x0F);
            memory.Write<short>(writeInto + 2, cityId);
            memory.Write<byte>(writeInto + 4, (byte)1);
            memory.Write<byte>(SaveGamePointer + CitySlotsOffset, ++cityLength);

        }
    }
}
