using Nep3ArchipelagoClient.Archipelago;
using Reloaded.Hooks;
using Reloaded.Hooks.Definitions;
using Reloaded.Hooks.Definitions.Enums;
using Reloaded.Hooks.Definitions.X86;
using Reloaded.Memory;
using System.Text;

namespace Nep3ArchipelagoClient.src.Hooks
{
    public class ShopHook
    {
        public static List<IAsmHook> _asmHooks = new();
        public static IReverseWrapper<CheckSell> _onCheckSell;

        [Function(new[] { FunctionAttribute.Register.eax}, FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.None)]
        public delegate int CheckSell(nuint pointer);
        public static unsafe int OnCheckSell(nuint eax)
        {
            var mem = Memory.Instance;
            nuint pointer = mem.Read<nuint>(eax+0x70)+4;
            pointer = mem.Read<nuint>(pointer);
            Console.WriteLine(mem.Read<int>(pointer+4));

            return 1;
        }

        public static void SetupHooks(IReloadedHooks hooks)
        {
            string[] loadText =
            {
                "use32",
                "push eax",
                "push ecx",
                "push edx",
                "push ebp",
                "push esi",
                "push edi",
                "mov eax,[esi+0x30]",
                $"{hooks.Utilities.GetAbsoluteCallMnemonics(OnCheckSell, out _onCheckSell)}",
                "pop edi",
                "pop esi",
                "pop ebp",
                "pop edx",
                "pop ecx",
                "push dword [esi+0x30]",
                "cmp eax,1",
                "jne short $+7",
                "call 0x00C205C0",
                "pop eax",
                //=
            };
            _asmHooks.Add(hooks.CreateAsmHook(loadText, (int)(Mod.ModuleBase + 0x17E7CC), AsmHookBehaviour.DoNotExecuteOriginal).Activate());
        }
    }
}
