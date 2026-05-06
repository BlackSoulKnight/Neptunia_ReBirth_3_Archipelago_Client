using Nep3ArchipelagoClient.Archipelago;
using Reloaded.Hooks;
using Reloaded.Hooks.Definitions;
using Reloaded.Hooks.Definitions.Enums;
using Reloaded.Hooks.Definitions.X86;
using Reloaded.Memory;
using System.Text;
using static Nep3ArchipelagoClient.src.Hooks.EnemyHooks;

namespace Nep3ArchipelagoClient.src.Hooks
{
    internal class TextHooks
    {
        public static bool DoReplaceText = false;
        public static List<IAsmHook> _asmHooks = new();
        public static byte[] ReplacementText = new byte[255];

        public static IReverseWrapper<GetEnemyDropString> _onGetEnemyDropString;
        public static IReverseWrapper<ChangeText> _onTextRead;

        public static void ClearReplacementText()
        {
            for (int i = 0; i < ReplacementText.Length; i++)
            {
                ReplacementText[i] = 0;
            }
        }

        //change text for item
        //TODO target only on gather / killed enemy
        [Function(new[] { FunctionAttribute.Register.eax }, FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
        public delegate int ChangeText(int originalText);
        public static unsafe int OnChangeText(int eax)
        {
            int stringPointer = Memory.Instance.Read<int>((nuint)(eax + 0x08));

            if (DoReplaceText)
                fixed (byte* p = ReplacementText)
                    stringPointer = (int)p;
            DoReplaceText = false;
            return stringPointer;
        }

        public static int counter = 0;
        [Function(new[] { FunctionAttribute.Register.eax }, FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
        public delegate int GetEnemyDropString(int dungeonID);
        public static unsafe int OnGetEnemyDropString(int eax)
        {
            Console.WriteLine($"Item number {counter}");
            ClearReplacementText();
            ReadOnlySpan<byte> text = Encoding.UTF8.GetBytes($"Item {counter}");
            text.ToArray().CopyTo(ReplacementText, 0);
            counter++;
            DoReplaceText = true;
            return eax;
        }
        public static void SetupHooks(IReloadedHooks hooks)
        {
            string[] loadText =
            {
                "use32",
                $"{hooks.Utilities.GetAbsoluteCallMnemonics(OnChangeText, out _onTextRead)}",
                "pop ebp",
                "ret",
                //=
            };
            _asmHooks.Add(hooks.CreateAsmHook(loadText, (int)(Mod.ModuleBase + 0xDEA60), AsmHookBehaviour.ExecuteAfter).Activate());

        }
    }
}
