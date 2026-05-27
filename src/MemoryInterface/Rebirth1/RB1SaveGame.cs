using Nep3ArchipelagoClient.Hooks;
using Nep3ArchipelagoClient.MemoryInterface;
using Nep3ArchipelagoClient.Neptunia_1_Data;
using Reloaded.Memory;


namespace Nep3ArchipelagoClient
{
    internal class RB1SaveGame : SaveGame
    {
        static Memory memory = Memory.Instance;
        Inventory Inventory;

        public RB1SaveGame()
        {
            SaveGameOffest = 0x459248;
            Inventory = new RB3Inventory(this);
            APSaveLocation = 0x1062C;
            PlanOffset = 0x443310;
            EventFlagOffset = 0x918;
        }
        public override int CurrentDungeon()
        {
            return memory.Read<int>(SaveGamePointer - 0x12B6F4);
        }

        protected override void DoSetupSaveFile()
        {
            if (!IsInit && IsEventFlagSet(102))
            {
                //debug stuff
                memory.Write<byte>(SaveGamePointer + APSaveLocation - 17, 1);
                for (int i = 1; i < 10; i++)
                    SetEventFlag(i, false);
                for (int i = 100; i < 110; i++)
                    SetEventFlag(i, false);
                UnlockGameFeatures();
                var startchar = Mod.APClient.GetStartingCharacter();
                AddPartyMember(startchar);
                if (startchar != (int)CharacterId.neptune)
                    RemovePartyMember((int)CharacterId.neptune);
                if (startchar != (int)CharacterId.compa)
                    RemovePartyMember((int)CharacterId.compa);
                InitGear();
#if DEBUG
                Test_CharacterUnlock();
                Test_DungeonUnlock();
                //Test_VGMRun();
#endif
            }
        }
        private void UnlockGameFeatures()
        {

            var flagPionter = SaveGamePointer + 3580;
            for (nuint i = 0; i < 6; i++)
            {
                memory.Write<byte>(flagPionter + i, 0xFF);
                memory.Write<byte>(flagPionter + i + 16, 0xFF);
            }
            for (short id = 2; id < 6; id++)
                UnlockCity(id);
        }
        public override void AddDungeon(short dungeonId)
        {
            nuint dungeonLenghtOffset = 0x1062C;
            nuint dungeonOffset = dungeonLenghtOffset+0x4;

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
            character->Armor = 1632;
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
            bool pudding = Inventory.FindItem(203, out int _);
            bool syringe = Inventory.FindItem(204, out int _);
            bool notebook = Inventory.FindItem(205, out int _);
            bool doll = Inventory.FindItem(206, out int _);
            bool drawing = Inventory.FindItem(210, out int _);
            if (pudding && syringe && notebook && doll && drawing)
            {

            }
        }
        void Test_VGMRun()
        {
            Inventory.AddItem(1655, 4);
            Inventory.AddItem(1305, 1);
            Inventory.AddItem(40, 99);

        }
        void Test_DungeonUnlock()
        {
            for (short i = 1; i < 37; i++)
            {
                AddDungeon(i);
            }
        }
        void Test_CharacterUnlock()
        {
            for (short i = 1; i < 30; i++)
            {
                RemovePartyMember(i);
            }
            Thread.Sleep(5000);
            for (short i = 1; i < 30; i++)
            {
                AddPartyMember(i);
            }
        }

        public override bool GoalAchieved(long APLocation)
        {
            return false;
        }

        public override void UnlockCity(short cityId)
        {
            
        }
    }
}
