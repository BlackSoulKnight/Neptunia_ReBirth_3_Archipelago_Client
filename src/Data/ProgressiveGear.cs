using Nep3ArchipelagoClient.MemoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nep3ArchipelagoClient.Neptunia_3_Data.ProgressiveGear;
using Nep3ArchipelagoClient;

namespace Nep3ArchipelagoClient.Neptunia_Data
{
    internal class ProgressiveGear
    {
        protected Dictionary<int, List<short>> GearList = new();
        protected byte Amount = 2;
        static Inventory Inventory => Mod.Inventory;
        public static Dictionary<int, ProgressiveGear> ProgressiveGears = new();
        public static HashSet<short> UsedItems = new();
        public static void InitRB3List()
        {
            Dictionary<int, ProgressiveGear> list = new();
            list.Add((int)Neptunia_3_Data.CharacterId.neptune, new NeptuneGear());
            list.Add((int)Neptunia_3_Data.CharacterId.nepgear, new NepgearGear());
            list.Add((int)Neptunia_3_Data.CharacterId.plutia, new PlutiaGear());
            list.Add((int)Neptunia_3_Data.CharacterId.peashy, new PeashyGear());
            list.Add((int)Neptunia_3_Data.CharacterId.blanc, new BlancGear());
            list.Add((int)Neptunia_3_Data.CharacterId.noire, new NoireGear());
            list.Add((int)Neptunia_3_Data.CharacterId.ram, new RamGear());
            list.Add((int)Neptunia_3_Data.CharacterId.uni, new UniGear());
            list.Add((int)Neptunia_3_Data.CharacterId.vert, new VertGear());
            list.Add((int)Neptunia_3_Data.CharacterId.rom, new RomGear());
            list.Add(11, new RB3ArmorGear());
            if (UsedItems == null)
                UsedItems = new();
            foreach (var character in list)
                foreach(var item in character.Value.GetAllItems())
                    UsedItems.Add(item);
            ProgressiveGears = list;
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
        protected short[] GetAllItems()
        {
            List<short> items = new();
            foreach(var tier in GearList)
            {
                items.AddRange(tier.Value);
            }
            return items.ToArray();
        }
    }
}
