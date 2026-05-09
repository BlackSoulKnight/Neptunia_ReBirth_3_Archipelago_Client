using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Nep3ArchipelagoClient.src.Neptunia_3_Data
{
    public struct ComboAttack
    {
        public short CharacterId;
        public short AttackId;
    }
    [InlineArray(15)]
    public struct ComboAttackEntry
    {
        ComboAttack entry;
    }

    [StructLayout(LayoutKind.Explicit)]
    public unsafe struct Character
    {

        //comments -> offset
        [FieldOffset(0x0)]
        public byte CurrentForm; //0
        [FieldOffset(0x8)]
        public byte Cha; //8 DO NOT CHANGE
        [FieldOffset(4)]
        public int Exp; //4
        [FieldOffset(12)]
        public fixed byte CharacterName[32]; //12 maybe to many character last char need to be 0x00 utf8
        [FieldOffset(68)]
        public int CurrentHP; //68
        [FieldOffset(76)]
        public int CurrentSP; //76
        [FieldOffset(176)]
        public int Weapon; //176
        [FieldOffset(180)]
        public int Armor;  //180
        [FieldOffset(184)]
        public int Ornament; //184
        [FieldOffset(188)]
        public int ClothingBody; //188
        [FieldOffset(192)]
        public int ClothingHead; //192
        [FieldOffset(196)]
        public int CpuC; //196
        [FieldOffset(200)]
        public int CpuH; //200
        [FieldOffset(204)]
        public int CpuB; //204
        [FieldOffset(208)]
        public int CpuS; //208
        [FieldOffset(212)]
        public int CpuW; //212
        [FieldOffset(216)]
        public int CpuL; //216
        //500 skill 16 bytes?
        [FieldOffset(1172)]
        public ComboAttackEntry ComboAttacks; //1172 Attacks go in order Rush -> Break -> Power -> next row
    }
}
