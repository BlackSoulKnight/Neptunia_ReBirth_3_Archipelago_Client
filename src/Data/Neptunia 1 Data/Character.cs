using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nep3ArchipelagoClient.Neptunia_1_Data
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
        public byte CurrentForm;
        [FieldOffset(4)]
        public byte Cha;
        [FieldOffset(8)]
        public fixed byte CharacterName[32];
        [FieldOffset(40)]
        public int Exp;
        [FieldOffset(44)]
        public short Unkown; //maybe what you get per level up, looks similar to charater id?

        [FieldOffset(46)]
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
        public int Unkown2; // not sure what this stat does
        [FieldOffset(120)]
        public int BaseLuc;
        [FieldOffset(124)]
        public int BaseMov;

        //resistence
        [FieldOffset(128)]
        public int UnkownRes; // or not im not sure
        [FieldOffset(132)]
        public int FireRes;
        [FieldOffset(136)]
        public int IceRes;
        [FieldOffset(136)]
        public int WindRes;
        [FieldOffset(136)]
        public int LightRes;
        // 20 bytes of nothing? maybe other res might be phy and magic and not in 128

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
