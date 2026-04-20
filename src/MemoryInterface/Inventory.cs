using Reloaded.Memory;


namespace Nep3ArchipelagoClient.MemoryInterface
{
    internal class Inventory
    {
        SaveGame SaveGameInstance;
        UIntPtr InventoryPointer => InventorySizePointer + 0x04;
        const int ItemLength = 0x04;
        const UIntPtr ItemCountOffsetPointer = 0x02;
        UIntPtr InventorySizePointer => SaveGameInstance.SaveGamePointer + 0xC7CC;
        public int CurrentInventoryCount => Memory.Instance.Read<short>(InventorySizePointer);

        UIntPtr ItemPosition(int slot)
        {
            return InventoryPointer + (nuint)(slot * ItemLength);
        }

        public Inventory(SaveGame saveGameInstance)
        {
            SaveGameInstance = saveGameInstance;
        }

        public byte GetItemCountAtSlot(int slot)
        {
            if (slot < 0)
                return 0;
            return Memory.Instance.Read<byte>(ItemPosition(slot)+ItemCountOffsetPointer);
        }
        public short GetItemIDAtSlot(int slot)
        {
            if (slot < 0)
                return 0;
            return Memory.Instance.Read<short>(ItemPosition(slot));
        }
        public bool FindItem(short itemID,out int position)
        {
            position = 0;
            if (CurrentInventoryCount <= 0)
                return false;
            for(int i = 0; i < CurrentInventoryCount; i++)
            {
                if(itemID == GetItemIDAtSlot(i))
                {
                    position = i;
                    return true;
                }
            }
            return false;
        }

        public void AddItem(short itemID,byte amount)
        {
            if(FindItem(itemID,out int pos))
            {
                byte currentAmount = GetItemCountAtSlot(pos);
                amount = (byte)Math.Min(99, Math.Max(currentAmount + amount, 0));
                Memory.Instance.Write<short>(ItemPosition(pos)+ItemCountOffsetPointer, amount);
            }
            else
            {
                if (amount > 99)
                    amount = 99;
                var newpos = ItemPosition(CurrentInventoryCount);
                Memory.Instance.Write<short>(newpos,itemID);
                Memory.Instance.Write<short>(newpos+ItemCountOffsetPointer,amount);
                Memory.Instance.Write<short>(InventorySizePointer, (short)(CurrentInventoryCount+1));
            }
        }
    }
}
