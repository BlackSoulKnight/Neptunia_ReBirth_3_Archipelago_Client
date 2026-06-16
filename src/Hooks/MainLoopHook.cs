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
        public static IReverseWrapper<MainLoop> _onMainLoop;
        public static IReverseWrapper<InitSaveGame> _onInitSavegame;
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
            if (Mod.SaveGame.CurrentDungeon() == 0 && Mod.SaveGame.WorldStateInDungeon())
                Mod.SaveGame.SetWorldState(3);
            Mod.APClient.Update(deltaTime);
            Mod.SaveGame.SetupSaveFile();
            Mod.SaveGame.CheckUnlockGoalCondition();
            return 0;
        }
        [Function(new[] { FunctionAttribute.Register.ecx, FunctionAttribute.Register.eax }, FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.None)]
        public delegate int InitSaveGame(nuint pointer,int eax);
        public static unsafe int OnInitSageGame(nuint ecx,int eax)
        {
            if (eax == 0)
                Mod.SaveGame.SaveGamePointer = ecx;
            else
                Mod.SaveGame.WorldStatePointer = ecx;
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
                $"{hooks.Utilities.GetAbsoluteCallMnemonics(OnMainLoop, out _onMainLoop)}",
                "popfd",
                "popad"
            };
            if (Mod.Game == NeptuniaGame.Neptunia_ReBirth_1)
            {
                if (FunctionScanner.FindFunction("MainLoop", "C7 45 ?? 00 00 00 00 C7 45 ?? 00 00 00 00 C7 45 ?? C0 03 00 00 C7 45 ?? 20 02 00 00 C7 45 ?? 00 04 00 00 E8", out offset))
                    _asmHooks.Add(hooks.CreateAsmHook(mainloop, (int)(Mod.ModuleBase + offset), AsmHookBehaviour.ExecuteFirst).Activate());
            }
            else
            {
                if (FunctionScanner.FindFunction("MainLoop", "E8 ?? ?? ?? ?? 83 C4 08 E8 ?? ?? ?? ?? 50 E8 ?? ?? ?? ?? 50 6A 00 6A 00 E8 ?? ?? ?? ?? 83 C4 10", out offset))
                    _asmHooks.Add(hooks.CreateAsmHook(mainloop, (int)(Mod.ModuleBase + offset), AsmHookBehaviour.ExecuteFirst).Activate());
            }
            string[] saveGame =
            {
                "use32",
                "pushad",
                "pushfd",
                "mov eax,0",
                $"{hooks.Utilities.GetAbsoluteCallMnemonics(OnInitSageGame, out _onInitSavegame)}",
                "popfd",
                "popad"
            };
            switch (Mod.Game)
            {
                case NeptuniaGame.Neptunia_ReBirth_1:
                    if (FunctionScanner.FindFunction("Init Savegame", "89 0D ?? ?? ?? ?? 85 C9 0F 84 ?? ?? ?? ?? 68 88 64 0C 00", out offset))
                        _asmHooks.Add(hooks.CreateAsmHook(saveGame, (int)(Mod.ModuleBase + offset), AsmHookBehaviour.ExecuteFirst).Activate());
                    break;
                case NeptuniaGame.Neptunia_ReBirth_2:
                    if (FunctionScanner.FindFunction("Init Savegame", "89 0D ?? ?? ?? ?? 85 C9 0F 84 ?? ?? ?? ?? 68 98 F3 0C 00", out offset))
                        _asmHooks.Add(hooks.CreateAsmHook(saveGame, (int)(Mod.ModuleBase + offset), AsmHookBehaviour.ExecuteFirst).Activate());
                    break;
                case NeptuniaGame.Neptunia_ReBirth_3:
                    if (FunctionScanner.FindFunction("Init Savegame", "89 0D ?? ?? ?? ?? 85 C9 0F 84 ?? ?? ?? ?? 68 28 BD 0F 00", out offset))
                        _asmHooks.Add(hooks.CreateAsmHook(saveGame, (int)(Mod.ModuleBase + offset), AsmHookBehaviour.ExecuteFirst).Activate());
                    break;
            }


            saveGame[3] = "mov eax,1";
            if (FunctionScanner.FindFunction("Init Worldstate", "89 0D ?? ?? ?? ?? 85 C9 0F 84 ?? ?? ?? ?? C7 81 ?? ?? ?? ?? 00 00 00 00", out offset))
                _asmHooks.Add(hooks.CreateAsmHook(saveGame, (int)(Mod.ModuleBase + offset), AsmHookBehaviour.ExecuteFirst).Activate());
        }
    }
}
