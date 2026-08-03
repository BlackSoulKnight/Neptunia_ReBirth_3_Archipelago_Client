using System.Runtime.InteropServices;


namespace Nep3ArchipelagoClient.Neptunia_Data
{
    [StructLayout(LayoutKind.Explicit)]
    public unsafe struct Item
    {
        [FieldOffset(0)]    public unsafe int MaybeType;
        [FieldOffset(4)]    public unsafe int ItemID;
        [FieldOffset(8)]    public unsafe int SPointer_Name;
        [FieldOffset(12)]   public unsafe short Model; //takes model depending on slot
        [FieldOffset(14)]   public unsafe short Variant;
        [FieldOffset(16)]   public fixed short CPUModel[32]; //charID1
        [FieldOffset(80)]   public fixed short CPUVariant[32]; //char 1
        [FieldOffset(144)]  public unsafe int xx90;
        [FieldOffset(148)]  public unsafe int xx94;
        [FieldOffset(152)]  public unsafe int xx98;
        [FieldOffset(156)]  public unsafe int CPU_Equipable_Charcters; //bitflags up to 32 chars
        [FieldOffset(160)]  public unsafe int InventoryCategory;
        [FieldOffset(164)]  public unsafe short SpriteIcon; //messes with shop
        [FieldOffset(166)]  public unsafe short StackSize;
        [FieldOffset(168)]  public unsafe int BuyPrice;
        [FieldOffset(172)]  public unsafe int SellPrice;
        [FieldOffset(176)]  public unsafe int IncreaseHP;
        [FieldOffset(180)]  public unsafe int xxB4;
        [FieldOffset(184)]  public unsafe int IncreaseSP;
        [FieldOffset(188)]  public unsafe int IncreaseSTR;
        [FieldOffset(192)]  public unsafe int IncreaseVIT;
        [FieldOffset(196)]  public unsafe int IncreaseINT;
        [FieldOffset(200)]  public unsafe int IncreaseMEN;
        [FieldOffset(204)]  public unsafe int IncreaseAGI;
        [FieldOffset(208)]  public unsafe int IncreaseTEC;
        [FieldOffset(212)]  public unsafe int IncreaseCritChance;
        [FieldOffset(216)]  public unsafe int IncreaseLUK;
        [FieldOffset(220)]  public unsafe int IncreaseMOV;
        [FieldOffset(224)]  public unsafe int xxE0;
        [FieldOffset(228)]  public unsafe int IncreaseFireResi;
        [FieldOffset(232)]  public unsafe int IncreaseColdResi;
        [FieldOffset(236)]  public unsafe int IncreaseWindResi;
        [FieldOffset(240)]  public unsafe int IncreaseLightningResi;
        [FieldOffset(244)]  public unsafe int xxF4;
        [FieldOffset(248)]  public unsafe int xxF8;
        [FieldOffset(252)]  public unsafe int xxFC;
        [FieldOffset(256)]  public unsafe int xx100;
        [FieldOffset(260)]  public unsafe int xx104;
        [FieldOffset(264)]  public unsafe short xx108;
        [FieldOffset(266)]  public unsafe short xx10A;
        [FieldOffset(268)]  public unsafe short xx10C;
        [FieldOffset(270)]  public unsafe short xx10E;
        [FieldOffset(272)]  public unsafe short xx110;
        [FieldOffset(274)]  public unsafe short xx112;
        [FieldOffset(276)]  public unsafe short xx114;
        [FieldOffset(278)]  public unsafe short xx116;
        [FieldOffset(280)]  public unsafe short xx118;
        [FieldOffset(282)]  public unsafe short xx11A;
        [FieldOffset(284)]  public unsafe short ChipLevel;
        [FieldOffset(286)]  public unsafe short UseAction;
        [FieldOffset(288)]  public unsafe int SPointer_Description;
    }
}
