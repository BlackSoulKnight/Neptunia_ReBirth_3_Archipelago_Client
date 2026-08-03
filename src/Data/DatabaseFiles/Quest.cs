using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Nep3ArchipelagoClient.Neptunia_Data
{
    [StructLayout(LayoutKind.Explicit)]
    public unsafe struct QuestObjective
    {
        [FieldOffset(0x0)]
        public int Objective;
        [FieldOffset(0x4)]
        public int Quantity;
    }
    [StructLayout(LayoutKind.Explicit)]
    public unsafe struct QuestReward
    {
        [FieldOffset(0x0)]
        public int Item;
        [FieldOffset(0x4)]
        public int Quantity;
    }
    [StructLayout(LayoutKind.Explicit)]
    public unsafe struct ColosseumEnemy
    {
        [FieldOffset(0x0)]
        public short Enemy;
        [FieldOffset(0x2)]
        public short Unkown;
        [FieldOffset(0x4)]
        public int Quantity;
    }
    [StructLayout(LayoutKind.Explicit, Size = 204)]
    public unsafe struct Quest
    {
        [FieldOffset(0)]
        public int QuestID;
        [FieldOffset(4)]
        public int StringPointerQuestName;
        [FieldOffset(8)]
        public int QuestType;
        [FieldOffset(12)]
        public QuestObjective Objective1;
        [FieldOffset(20)]
        public QuestObjective Objective2;
        [FieldOffset(28)]
        public QuestObjective Objective3;
        [FieldOffset(36)]
        public QuestObjective Objective4;
        [FieldOffset(44)]
        public int MoneyReward;
        [FieldOffset(48)]
        public int Unkown;
        [FieldOffset(52)]
        public QuestReward Reward1;
        [FieldOffset(60)]
        public QuestReward Reward2;
        [FieldOffset(68)]
        public QuestReward Reward3;
        [FieldOffset(76)]
        public byte NationReceive;
        [FieldOffset(77)]
        public byte NationGive;
        [FieldOffset(78)]
        public short ShareAmount;

        //66 bytes

        [FieldOffset(146)]
        public ColosseumEnemy Enemy1;
        [FieldOffset(154)]
        public ColosseumEnemy Enemy2;
        [FieldOffset(162)]
        public ColosseumEnemy Enemy3;
        [FieldOffset(170)]
        public ColosseumEnemy Enemy4;
        [FieldOffset(178)]
        public ColosseumEnemy Enemy5;

        //6 bytes

        [FieldOffset(192)]
        public int SPointerOrigin;
        [FieldOffset(196)]
        public int SPointerClient;
        [FieldOffset(200)]
        public int SPointerDescription;
    }
}