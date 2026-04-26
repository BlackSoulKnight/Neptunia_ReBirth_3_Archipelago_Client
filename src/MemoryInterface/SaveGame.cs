using System;
using System.Collections.Generic;
using Nep3ArchipelagoClient.src.Hooks;
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
