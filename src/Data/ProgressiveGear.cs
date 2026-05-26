using Nep3ArchipelagoClient.MemoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            list.Add((int)Neptunia_3_Data.CharacterId.neptune,          new Neptunia_3_Data.ProgressiveGear.NeptuneGear());
            list.Add((int)Neptunia_3_Data.CharacterId.nepgear,          new Neptunia_3_Data.ProgressiveGear.NepgearGear());
            list.Add((int)Neptunia_3_Data.CharacterId.plutia,           new Neptunia_3_Data.ProgressiveGear.PlutiaGear());
            list.Add((int)Neptunia_3_Data.CharacterId.peashy,           new Neptunia_3_Data.ProgressiveGear.PeashyGear());
            list.Add((int)Neptunia_3_Data.CharacterId.blanc,            new Neptunia_3_Data.ProgressiveGear.BlancGear());
            list.Add((int)Neptunia_3_Data.CharacterId.noire,            new Neptunia_3_Data.ProgressiveGear.NoireGear());
            list.Add((int)Neptunia_3_Data.CharacterId.ram,              new Neptunia_3_Data.ProgressiveGear.RamGear());
            list.Add((int)Neptunia_3_Data.CharacterId.uni,              new Neptunia_3_Data.ProgressiveGear.UniGear());
            list.Add((int)Neptunia_3_Data.CharacterId.vert,             new Neptunia_3_Data.ProgressiveGear.VertGear());
            list.Add((int)Neptunia_3_Data.CharacterId.rom,              new Neptunia_3_Data.ProgressiveGear.RomGear());
            list.Add(11, new Neptunia_3_Data.ProgressiveGear.RB3ArmorGear());
            if (UsedItems == null)
                UsedItems = new();
            foreach (var character in list)
                foreach(var item in character.Value.GetAllItems())
                    UsedItems.Add(item);
            ProgressiveGears = list;
        }
        public static void InitRB2List()
        {
            Dictionary<int, ProgressiveGear> list = new();
            list.Add((int)Neptunia_2_Data.CharacterId.neptune,          new Neptunia_2_Data.ProgressiveGear.NeptuneGear());
            list.Add((int)Neptunia_2_Data.CharacterId.nepgear,          new Neptunia_2_Data.ProgressiveGear.NepgearGear());
            list.Add((int)Neptunia_2_Data.CharacterId.blanc,            new Neptunia_2_Data.ProgressiveGear.BlancGear());
            list.Add((int)Neptunia_2_Data.CharacterId.noire,            new Neptunia_2_Data.ProgressiveGear.NoireGear());
            list.Add((int)Neptunia_2_Data.CharacterId.ram,              new Neptunia_2_Data.ProgressiveGear.RamGear());
            list.Add((int)Neptunia_2_Data.CharacterId.uni,              new Neptunia_2_Data.ProgressiveGear.UniGear());
            list.Add((int)Neptunia_2_Data.CharacterId.vert,             new Neptunia_2_Data.ProgressiveGear.VertGear());
            list.Add((int)Neptunia_2_Data.CharacterId.rom,              new Neptunia_2_Data.ProgressiveGear.RomGear());
            list.Add((int)Neptunia_2_Data.CharacterId.broccoli,         new Neptunia_2_Data.ProgressiveGear.BroccoliGear());
            list.Add((int)Neptunia_2_Data.CharacterId.cave,             new Neptunia_2_Data.ProgressiveGear.CaveGear());
            list.Add((int)Neptunia_2_Data.CharacterId.chika,            new Neptunia_2_Data.ProgressiveGear.ChikaGear());
            list.Add((int)Neptunia_2_Data.CharacterId.compa,            new Neptunia_2_Data.ProgressiveGear.CompaGear());
            list.Add((int)Neptunia_2_Data.CharacterId.cyberconnect2,    new Neptunia_2_Data.ProgressiveGear.CyberConnect2Gear());
            list.Add((int)Neptunia_2_Data.CharacterId.falcom,           new Neptunia_2_Data.ProgressiveGear.FalcomGear());
            list.Add((int)Neptunia_2_Data.CharacterId.fivepb,           new Neptunia_2_Data.ProgressiveGear.FivePbGear());
            list.Add((int)Neptunia_2_Data.CharacterId.histoire,         new Neptunia_2_Data.ProgressiveGear.HistoireGear());
            list.Add((int)Neptunia_2_Data.CharacterId.IF,               new Neptunia_2_Data.ProgressiveGear.IFGear());
            list.Add((int)Neptunia_2_Data.CharacterId.kei,              new Neptunia_2_Data.ProgressiveGear.KeiGear());
            list.Add((int)Neptunia_2_Data.CharacterId.marvy,            new Neptunia_2_Data.ProgressiveGear.MarvyGear());
            list.Add((int)Neptunia_2_Data.CharacterId.mina,             new Neptunia_2_Data.ProgressiveGear.MinaGear());
            list.Add((int)Neptunia_2_Data.CharacterId.red,              new Neptunia_2_Data.ProgressiveGear.RedGear());
            list.Add((int)Neptunia_2_Data.CharacterId.tekken,           new Neptunia_2_Data.ProgressiveGear.TekkenGear());
            list.Add(26, new Neptunia_2_Data.ProgressiveGear.RB2ArmorGear());
            if (UsedItems == null)
                UsedItems = new();
            foreach (var character in list)
                foreach(var item in character.Value.GetAllItems())
                    UsedItems.Add(item);
            ProgressiveGears = list;
        }
        public static void InitRB1List()
        {
            Dictionary<int, ProgressiveGear> list = new();
            list.Add((int)Neptunia_1_Data.CharacterId.neptune,          new Neptunia_1_Data.ProgressiveGear.NeptuneGear());
            list.Add((int)Neptunia_1_Data.CharacterId.nepgear,          new Neptunia_1_Data.ProgressiveGear.NepgearGear());
            list.Add((int)Neptunia_1_Data.CharacterId.blanc,            new Neptunia_1_Data.ProgressiveGear.BlancGear());
            list.Add((int)Neptunia_1_Data.CharacterId.noire,            new Neptunia_1_Data.ProgressiveGear.NoireGear());
            list.Add((int)Neptunia_1_Data.CharacterId.ram,              new Neptunia_1_Data.ProgressiveGear.RamGear());
            list.Add((int)Neptunia_1_Data.CharacterId.uni,              new Neptunia_1_Data.ProgressiveGear.UniGear());
            list.Add((int)Neptunia_1_Data.CharacterId.vert,             new Neptunia_1_Data.ProgressiveGear.VertGear());
            list.Add((int)Neptunia_1_Data.CharacterId.rom,              new Neptunia_1_Data.ProgressiveGear.RomGear());
            list.Add((int)Neptunia_1_Data.CharacterId.broccoli,         new Neptunia_1_Data.ProgressiveGear.BroccoliGear());
            list.Add((int)Neptunia_1_Data.CharacterId.cave,             new Neptunia_1_Data.ProgressiveGear.CaveGear());
            list.Add((int)Neptunia_1_Data.CharacterId.chika,            new Neptunia_1_Data.ProgressiveGear.ChikaGear());
            list.Add((int)Neptunia_1_Data.CharacterId.compa,            new Neptunia_1_Data.ProgressiveGear.CompaGear());
            list.Add((int)Neptunia_1_Data.CharacterId.cyberconnect2,    new Neptunia_1_Data.ProgressiveGear.CyberConnect2Gear());
            list.Add((int)Neptunia_1_Data.CharacterId.falcom,           new Neptunia_1_Data.ProgressiveGear.FalcomGear());
            list.Add((int)Neptunia_1_Data.CharacterId.fivepb,           new Neptunia_1_Data.ProgressiveGear.FivePbGear());
            list.Add((int)Neptunia_1_Data.CharacterId.histoire,         new Neptunia_1_Data.ProgressiveGear.HistoireGear());
            list.Add((int)Neptunia_1_Data.CharacterId.IF,               new Neptunia_1_Data.ProgressiveGear.IFGear());
            list.Add((int)Neptunia_1_Data.CharacterId.kei,              new Neptunia_1_Data.ProgressiveGear.KeiGear());
            list.Add((int)Neptunia_1_Data.CharacterId.marvy,            new Neptunia_1_Data.ProgressiveGear.MarvyGear());
            list.Add((int)Neptunia_1_Data.CharacterId.mina,             new Neptunia_1_Data.ProgressiveGear.MinaGear());
            list.Add((int)Neptunia_1_Data.CharacterId.red,              new Neptunia_1_Data.ProgressiveGear.RedGear());
            list.Add((int)Neptunia_1_Data.CharacterId.tekken,           new Neptunia_1_Data.ProgressiveGear.TekkenGear());
            list.Add(26, new Neptunia_1_Data.ProgressiveGear.RB2ArmorGear());
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
