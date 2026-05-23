using Nep3ArchipelagoClient.Archipelago;
using Nep3ArchipelagoClient.MemoryInterface;
using Reloaded.Hooks;
using Reloaded.Hooks.Definitions;
using Reloaded.Hooks.Definitions.Enums;
using Reloaded.Hooks.Definitions.X86;
using Reloaded.Memory;
using System.Text;
using Nep3ArchipelagoClient.Data.Neptunia_2_Data;

namespace Nep3ArchipelagoClient.Hooks
{
    internal class RB2EventHooks
    {
        public static List<IAsmHook> _asmHooks = new();
        public static byte[] ReplacementText => TextHooks.ReplacementText;

        public static IReverseWrapper<ChangeText> _onTextRead;

        [Function(new[] { FunctionAttribute.Register.edi }, FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
        public delegate int ChangeText(nuint eventPointer);
        public static unsafe int OnTestEvent(nuint edi)
        {
            //selfcheck -> edi+0xC
            //custom checks
            var currEvent = Memory.Instance.Read<int>(edi + 0xC + 0x4);
            Console.WriteLine($"Something with event {currEvent}");
            bool self = Mod.SaveGame.IsEventFlagSet(currEvent);
            if (self || !Events.AllowedEvents.Contains(currEvent))
                return 0;
            return 1;
        }

        public static void SetupHooks(IReloadedHooks hooks)
        {
            nuint offset = 0;
            //text replacement test

            if (FunctionScanner.JumpTarget("No Event", "E8 ?? ?? ?? ?? 3B F0 0F 83", out nuint jmptargetFalse)&&
                FunctionScanner.JumpTarget("No Event", "83 7F ?? 00 75 ?? 83 C6 04", out nuint jmptargetTrue))
            {
                string[] testReplacement =
                {
                "use32",
                "add esp,0x4",
                "push ecx",
                "push edx",
                "push ebp",
                "push esi",
                "push edi",
                "pushfd",
                $"{hooks.Utilities.GetAbsoluteCallMnemonics(OnTestEvent, out _onTextRead)}",
                "popfd",
                "pop edi",
                "pop esi",
                "pop ebp",
                "pop edx",
                "pop ecx",
                "test eax,eax",
                $"jne true",
                $"mov eax,{Mod.ModuleBase+jmptargetFalse}",
                $"jmp eax",
                "true:",
                $"mov eax,{Mod.ModuleBase+jmptargetTrue}",
                $"jmp eax",
                //=
            };
                if (FunctionScanner.FindFunction("Event Logic", "8B 3E 8D 47 ?? 50 E8 ?? ?? ?? ?? 83 C4 04 84 C0 0F 84 ?? ?? ?? ?? 8D 47 ?? 50 E8 ?? ?? ?? ?? 83 C4 04 84 C0 0F 84", out offset))
                    _asmHooks.Add(hooks.CreateAsmHook(testReplacement, (int)(Mod.ModuleBase + offset), AsmHookBehaviour.ExecuteAfter).Activate());
            }


        }
    }
}
