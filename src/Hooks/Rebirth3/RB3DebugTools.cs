using Nep3ArchipelagoClient.Archipelago;
using Nep3ArchipelagoClient.MemoryInterface;
using Reloaded.Hooks.Definitions;
using Reloaded.Hooks.Definitions.Enums;
using Reloaded.Hooks.Definitions.X86;
using Reloaded.Memory;
using System.Text;


namespace Nep3ArchipelagoClient.Hooks.Rebirth3
{
    internal class RB3DebugTools
    {
        public static List<IAsmHook> _asmHooks = new();

        public static IReverseWrapper<EventLoad> _onLoadEvent;

        [Function(new[] { FunctionAttribute.Register.eax }, FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
        public delegate int EventLoad(int eventID);
        public static unsafe int OnEventLoad(int eax)
        {
            Console.WriteLine($"Load Event ID:{eax}");
            return eax;
        }

        public static void SetupHooks(IReloadedHooks hooks)
        {
            if (hooks == null) return;
            // Game functions
            nuint offset = 0;
            string[] removeDungeonCreation = {
                "use32",
                "pushad",
                "pushfd",
                "mov eax,[esp+0x24]",
                $"{hooks.Utilities.GetAbsoluteCallMnemonics(OnEventLoad, out _onLoadEvent)}",
                "popfd",
                "popad",
            };
            if (FunctionScanner.FindFunction("EventFlags", "E8 ?? ?? ?? ?? 83 C4 08 8B 4D ?? 33 C0 39 46", out offset))
                _asmHooks.Add(hooks.CreateAsmHook(removeDungeonCreation, (int)(Mod.ModuleBase + offset), AsmHookBehaviour.ExecuteFirst).Activate());
        }

    }
}
