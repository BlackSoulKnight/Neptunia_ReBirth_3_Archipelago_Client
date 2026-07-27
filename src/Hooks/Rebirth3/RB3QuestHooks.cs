using Nep3ArchipelagoClient.MemoryInterface;
using Reloaded.Hooks.Definitions;
using Reloaded.Hooks.Definitions.Enums;
using Reloaded.Hooks.Definitions.X86;
using Reloaded.Memory;
using static Nep3ArchipelagoClient.Hooks.CharacterHooks;



namespace Nep3ArchipelagoClient.Hooks.Rebirth3
{
    internal class RB3QuestHooks
    {
        public static List<IAsmHook> _asmHooks = new();

        public static IReverseWrapper<EventLoad> _onQuestReward;

        [Function(new[] { FunctionAttribute.Register.eax}, FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
        public delegate int EventLoad(int eventID);
        public static unsafe int OnQuestReward(int eax)
        {
            Console.WriteLine($"Get Quest Reward from Quest ID {eax}");
            Mod.APClient.SendLocation(eax + Archipelago.APClient.QuestBaseID);
            return eax;
        }

        public static IFunction<AddNewQuest> _addNewQuest;

        [Function(CallingConventions.Stdcall)]
        public delegate int AddNewQuest(int param1);

        public static void SetupHooks(IReloadedHooks hooks)
        {
            if (hooks == null) return;
            // Game functions
            nuint offset = 0;
            nuint[] offsets;
            string[] Event = {
                "use32",
                "pushad",
                "pushfd",
                "mov eax,[esp+0x20]",
                $"{hooks.Utilities.GetAbsoluteCallMnemonics(OnQuestReward, out _onQuestReward)}",
                "popfd",
                "popad",
            };
            if (FunctionScanner.FindFunction("Complete Reward", "55 8B EC 8B 0D ?? ?? ?? ?? 83 EC 08 8B 81", out offset))
                _asmHooks.Add(hooks.CreateAsmHook(Event, (int)(Mod.ModuleBase + offset), AsmHookBehaviour.ExecuteFirst).Activate());

            if (FunctionScanner.FindFunction("Quest Reward add to Inventory", "E8 ?? ?? ?? ?? 83 C4 14 46 83 FE 03", out offset))
            {
                string[] IgnoreItems = {
                    "use32",
                    $"add esp,0x14",
                };
                _asmHooks.Add(hooks.CreateAsmHook(IgnoreItems, (int)(Mod.ModuleBase + offset), AsmHookBehaviour.DoNotExecuteOriginal).Activate());
            }

            if (FunctionScanner.FindFunction("Add Quest", "55 8B EC 56 8B 75 ?? 57 56 E8 ?? ?? ?? ?? 8B F8 83 C4 04 85 FF 75 ?? 5F 32 C0", out offset))
                _addNewQuest = hooks.CreateFunction<AddNewQuest>((int)(Mod.ModuleBase + offset));

        }

    }
}
