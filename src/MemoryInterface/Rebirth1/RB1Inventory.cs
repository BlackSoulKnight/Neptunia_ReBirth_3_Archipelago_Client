using Nep3ArchipelagoClient.Hooks;
using Nep3ArchipelagoClient.MemoryInterface;
using Reloaded.Memory;


namespace Nep3ArchipelagoClient.MemoryInterface
{
    internal class RB1Inventory : Inventory
    {
        public RB1Inventory(SaveGame savegame) : base(savegame)
        {
            InventorySizeOffset = 0xCA46;

        }
    }
}
