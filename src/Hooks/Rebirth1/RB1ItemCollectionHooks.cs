using Nep3ArchipelagoClient.Archipelago;
using Nep3ArchipelagoClient.MemoryInterface;
using Reloaded.Hooks.Definitions;
using Reloaded.Hooks.Definitions.Enums;
using Reloaded.Hooks.Definitions.X86;
using Reloaded.Memory;
using System.Text;

namespace Nep3ArchipelagoClient.Hooks.Rebirth1
{
    public class RB1ItemCollectionHooks
    {
        public static List<IAsmHook> _asmHooks = new();
        static bool allowOrignalLoot => ItemCollectionHooks.allowOrignalLoot;

        public static IReverseWrapper<CollectGatherSpot> _onCollectionGatherSpot;
        public static IFunction<ItemCollectionHooks.AddItemToInventory> _addItemFunction => ItemCollectionHooks._addItemFunction;


        public static void SetupHooks(IReloadedHooks hooks)
        {
            if (hooks == null) return;
            // Game functions
            nuint offset = 0;
            string[] collectGatherSpot = {
                "use32",
                "push dword [eax]",
                "pushad",
                "pushfd",
                "mov eax,[esp+0x24]",
                "mov ebx,[esp+0x28]",
                $"{hooks.Utilities.GetAbsoluteCallMnemonics(OnCollectGatherSpot, out _onCollectionGatherSpot)}",
                "popfd",
                "popad",
                "",
            };
            if (FunctionScanner.FindFunction("Collect Gather", "FF 30 E8 ?? ?? ?? ?? 8B 35", out offset))
                _asmHooks.Add(hooks.CreateAsmHook(collectGatherSpot, (int)(Mod.ModuleBase + offset), AsmHookBehaviour.DoNotExecuteOriginal).Activate());


            string[] removeDungeonCreation = {
                "use32",
                "ret",
            };
            if (FunctionScanner.FindFunction("Create Dungeon", "55 8B EC 53 8B 5D ?? 85 DB 75 ?? 32 C0 5B 5D C3 A1 ?? ?? ?? ?? 57 8B B8 ?? ?? ?? ?? 83 FF 50", out offset))
                _asmHooks.Add(hooks.CreateAsmHook(removeDungeonCreation, (int)(Mod.ModuleBase + offset), AsmHookBehaviour.DoNotExecuteOriginal).Activate());
        }

        //determines if an item need to be set in the players inventory
        [Function(new FunctionAttribute.Register[] { FunctionAttribute.Register.eax, FunctionAttribute.Register.ebx }, FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
        public delegate int CollectGatherSpot(uint item, uint quantity);
        public static unsafe int OnCollectGatherSpot(uint eax, uint ebx)
        {
            Console.WriteLine($"item id:{eax} quantity:{ebx}");
            //non randomized item
            if (allowOrignalLoot)
                _addItemFunction.GetWrapper()(eax, ebx, (char)1);

            return (int)eax;
        }


    }
}
