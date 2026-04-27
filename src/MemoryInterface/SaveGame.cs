using System;
using System.Collections.Generic;
using Nep3ArchipelagoClient.src.Hooks;
using Nep3ArchipelagoClient.src.Neptunia_3_Data;
using Reloaded.Memory;

namespace Nep3ArchipelagoClient
{
    internal class SaveGame
    {
        public UIntPtr SaveGamePointer;

        Memory memory = Memory.Instance;

        public SaveGame(UIntPtr baseAddress)
        {
            while (SaveGamePointer == 0)
            {
                SaveGamePointer = Memory.Instance.Read<uint>(Mod.ModuleBase + 0x4F6ED8);
                Thread.Sleep(100);
            }
        }
        public int CurrentItemCount()
        {
            return memory.Read<int>(SaveGamePointer + 0xC7CC);
        }
        public int CurrentDungeon()
        {
            return memory.Read<int>(SaveGamePointer - 0x1EA8FA);
        }

        public bool DoOnceAfterChapter1Start = true;
        public void SetupAllNations()
        {
            if (DoOnceAfterChapter1Start && (memory.Read<byte>(SaveGamePointer + 0x928) & 1<<5) > 0)
            {
                var worldMapThing = memory.Read<byte>(SaveGamePointer + 0xE04);
                worldMapThing |= 1 << 4;
                memory.Write<byte>(SaveGamePointer + 0xE04, worldMapThing);
                DoOnceAfterChapter1Start = false;
                memory.Write<byte>(SaveGamePointer + 0xfe44, 9);
                var firstItemAt = SaveGamePointer + 0xfe48 + 6;
                var target = SaveGamePointer + 0xfe48 + 6 * 9;
                byte nationIdx = 1;
                for (var item = SaveGamePointer + 0xfe48+6; item < target; item += 6)
                {
                    memory.Write<byte>(item, 0x0A);
                    memory.Write<byte>(item + 1, 0x01);
                    memory.Write<byte>(item + 2, nationIdx);
                    nationIdx++;
                }
            }
        }

        public void AddDungeon(byte dungeonId)
        {
            if (dungeonId == 66) return;
            var dungeonListLength = memory.Read<byte>(SaveGamePointer + 0x103b0);
            var writeInto = SaveGamePointer + (nuint)(0x103b4 + 0x203c * dungeonListLength);
            memory.Write<byte>(writeInto, 0x0F);
            memory.Write<byte>(writeInto + 2, dungeonId);
            var nation = DungeonToNation.GetNation(dungeonId);
            memory.Write<byte>(writeInto + 4, (byte) nation); // 1 needs to replaced soonish for the nation id
            memory.Write<byte>(SaveGamePointer + 0x103b0, ++dungeonListLength);

        }
        public void test()
        {
            Memory memory = Memory.Instance;
            UIntPtr jmpCounter = SaveGamePointer + 0xE88;
            Console.WriteLine($"Base:{Mod.ModuleBase} Inventory:{SaveGamePointer} JumpCounter:{0xE88} Result:{jmpCounter}");
            Console.WriteLine(Memory.Instance.Read<uint>(Mod.ModuleBase + 0x4F6ED8));

            Console.WriteLine($"Inventory size = {memory.Read<int>(SaveGamePointer+0xC7CC)}");
            Console.WriteLine($"Jump Count = {memory.Read<int>(jmpCounter)}");

        }

        public static void AddItem(int id,int quantity) => ItemCollection._addItemFunction.GetWrapper()((uint)id, (uint)quantity, (char)1);
    }
}
