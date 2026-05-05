using Nep3ArchipelagoClient.MemoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nep3ArchipelagoClient.src.Neptunia_3_Data;

namespace Nep3ArchipelagoClient.src.Neptunia_3_Data.ProgressiveGear
{
    internal class ProgressiveGear
    {
        protected Dictionary<int, List<short>> GearList = new();
        protected byte Amount = 2;
        static Inventory Inventory => Mod.Inventory;
        public static Dictionary<CharacterId,ProgressiveGear> ProgressiveGears = InitList();

        static Dictionary<CharacterId, ProgressiveGear> InitList()
        {
            Dictionary<CharacterId, ProgressiveGear> list = new();
            list.Add(CharacterId.neptune, new NeptuneGear());
            list.Add(CharacterId.nepgear, new NepgearGear());
            list.Add(CharacterId.plutia, new PlutiaGear());
            list.Add(CharacterId.peashy, new PeashyGear());
            list.Add(CharacterId.blanc, new BlancGear());
            list.Add(CharacterId.noire, new NoireGear());
            list.Add(CharacterId.ram, new RamGear());
            list.Add(CharacterId.uni, new UniGear());
            list.Add(CharacterId.vert, new VertGear());
            list.Add(CharacterId.rom, new RomGear());
            list.Add(CharacterId.all, new ArmorGear());
            return list;
        }
        public void IncreaseGearTier()
        {
            int tier = 0;
            while (tier < GearList.Count && Inventory.FindItem(GearList[tier][0], out int _))
                tier++;
            if(tier<GearList.Count)
                foreach (var item in GearList[tier])
                        Inventory.AddItem(item, Amount);
        }

        public void UnlockTier(int tier)
        {
            if(GearList.ContainsKey(tier))
                foreach (var item in GearList[tier])
                    Inventory.AddItem(item, Amount);
        }
    }
}
