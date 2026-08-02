using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Nep3ArchipelagoClient.Neptunia_2_Data
{
    [StructLayout(LayoutKind.Explicit,Size = 0x58)]
    public unsafe struct CharacterLevelUp
    {
        [FieldOffset(0x0)]
        public int NextLevelRequirement;
        [FieldOffset(0x4)]
        public int IncreaseHP;
        [FieldOffset(0x8)]
        public int IncreaseCP;
        [FieldOffset(0xC)]
        public int IncreaseSP;
        [FieldOffset(0x10)]
        public int IncreaseSTR;
        [FieldOffset(0x14)]
        public int IncreaseVIT;
        [FieldOffset(0x18)]
        public int IncreaseINT;
        [FieldOffset(0x1C)]
        public int IncreaseMEN;
        [FieldOffset(0x20)]
        public int IncreaseAGI;
        [FieldOffset(0x24)]
        public int IncreaseTEC;
        [FieldOffset(0x28)]
        public int Unkown;
        [FieldOffset(0x2C)]
        public int IncreaseLUK;

        // Unknown 28 30 34 38 3C 40 44 48 4C 50 54 58
        // Size 0x58

    }
}
