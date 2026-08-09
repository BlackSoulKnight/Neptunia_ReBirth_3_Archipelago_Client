using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Nep3ArchipelagoClient.Neptunia_2_Data
{

    public struct ComboAttack
    {
        public short RushCharacterId;
        public short RushAttackId;
        public short BreakCharacterId;
        public short BreakAttackId;
        public short PowerCharacterId;
        public short PowerAttackId;
    }
    [InlineArray(5)]
    public struct ComboAttackEntry
    {
        ComboAttack entry;
    }

    [StructLayout(LayoutKind.Explicit)]
    public unsafe struct Character
    {
        [FieldOffset(0x0)]
        public short CurrentForm;
        [FieldOffset(0x8)]
        public fixed byte CharacterName[32];
        [FieldOffset(0x28)]
        public int Exp;
        [FieldOffset(0x2C)]
        public short Unkown; //maybe what you get per level up, looks similar to charater id?

        [FieldOffset(0x2E)]
        public short Level;
        [FieldOffset(64)]
        public int CurrentHP;
        [FieldOffset(72)]
        public int CurrentSP;
        [FieldOffset(80)]
        public int MaxBaseHP;
        [FieldOffset(84)]
        public int MaxCP;
        [FieldOffset(88)]
        public int MaxBaseSP;

        [FieldOffset(92)]
        public int BaseStr;
        [FieldOffset(96)]
        public int BaseVit;
        [FieldOffset(100)]
        public int BaseInt;
        [FieldOffset(104)]
        public int BaseMen;
        [FieldOffset(108)]
        public int BaseAgi;
        [FieldOffset(112)]
        public int BaseTec;
        [FieldOffset(116)]
        public int AVD; // unused
        [FieldOffset(120)]
        public int BaseLuc;
        [FieldOffset(124)]
        public int BaseMov;

        //resistence
        [FieldOffset(0x80)]
        public int PhysRes; // blank icon
        [FieldOffset(0x84)]
        public int FireRes;
        [FieldOffset(0x88)]
        public int IceRes;
        [FieldOffset(0x8C)]
        public int WindRes;
        [FieldOffset(0x90)]
        public int LightRes;
        // 20 bytes of nothing? maybe other res might be phy and magic and not in 128
        [FieldOffset(0x94)]
        public int NormalPhysRes;
        [FieldOffset(0x98)]
        public int NormalFireRes;
        [FieldOffset(0x9C)]
        public int NormalIceRes;
        [FieldOffset(0xA0)]
        public int NormalWindRes;
        [FieldOffset(0xA4)]
        public int NormalLightRes;

        [FieldOffset(168)]
        public int Unkown3;

        [FieldOffset(172)]
        public int Weapon;
        [FieldOffset(176)]
        public int Armor;
        [FieldOffset(180)]
        public int Ornament;
        [FieldOffset(184)]
        public int ClothingBody;
        [FieldOffset(188)]
        public int ClothingHead;
        [FieldOffset(192)]
        public int CpuC;
        [FieldOffset(196)]
        public int CpuH;
        [FieldOffset(200)]
        public int CpuB;
        [FieldOffset(204)]
        public int CpuS;
        [FieldOffset(208)]
        public int CpuW;
        [FieldOffset(212)]
        public int CpuL;
        [FieldOffset(1168)]
        public ComboAttackEntry ComboAttacks;
    }
}
