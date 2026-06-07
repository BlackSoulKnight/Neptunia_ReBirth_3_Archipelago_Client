using Nep3ArchipelagoClient.Archipelago;
using Nep3ArchipelagoClient.MemoryInterface;
using Nep3ArchipelagoClient.Neptunia_Data;
using Reloaded.Hooks;
using Reloaded.Hooks.Definitions;
using Reloaded.Hooks.Definitions.Enums;
using Reloaded.Hooks.Definitions.X86;
using Reloaded.Memory;
using System.Diagnostics;
using System.Text;
using System.Timers;

namespace Nep3ArchipelagoClient.Hooks
{
    public class MainLoopHook
    {
        public static List<IAsmHook> _asmHooks = new();
        public static IReverseWrapper<MainLoop> _onCheckSell;
        public static IFunction<SellItem> _sellItem;

        [Function(CallingConventions.Stdcall)]
        public delegate int SellItem(nuint itemStackPointer);

        static Stopwatch _timer = new();
        static double deltaTime; // in milliseconds
        static double timer = 0;
        [Function(new[] { FunctionAttribute.Register.eax }, FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.None)]
        public delegate int MainLoop(nuint pointer);
        public static unsafe int OnMainLoop(nuint eax)
        {
            if (Mod.SaveGame.SaveGamePointer == 0) return 0;
            _timer.Stop();
            deltaTime = _timer.Elapsed.TotalMilliseconds;
            _timer.Restart();
            
            Mod.APClient.Update(deltaTime);
            Mod.SaveGame.SetupSaveFile();
            Mod.SaveGame.CheckUnlockGoalCondition();
            return 0;
        }

        public static void SetupHooks(IReloadedHooks hooks)
        {
            nuint offset = 0;
            string[] mainloop =
            {
                "use32",
                "pushad",
                "pushfd",
                $"{hooks.Utilities.GetAbsoluteCallMnemonics(OnMainLoop, out _onCheckSell)}",
                "popfd",
                "popad"
            };
            if (FunctionScanner.FindFunction("MainLoop", "E8 ?? ?? ?? ?? 83 C4 08 E8 ?? ?? ?? ?? 50 E8 ?? ?? ?? ?? 50 6A 00 6A 00 E8 ?? ?? ?? ?? 83 C4 10", out offset))
                _asmHooks.Add(hooks.CreateAsmHook(mainloop, (int)(Mod.ModuleBase + offset), AsmHookBehaviour.ExecuteFirst).Activate());
        }
    }
}
