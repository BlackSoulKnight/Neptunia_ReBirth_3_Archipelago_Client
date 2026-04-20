using Nep3ArchipelagoClient;
using Reloaded.Hooks;
using Reloaded.Hooks.Definitions;
using Reloaded.Hooks.Definitions.Enums;
using Reloaded.Hooks.Definitions.Structs;
using Reloaded.Hooks.Definitions.X86;
using Reloaded.Memory;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;

namespace Nep3ArchipelagoClient.src.Hooks
{
    public class ItemCollection
    {
        public static byte[] ReplacementText = new byte[255];
        public static bool DoReplaceText = false;
        public static bool IsAPItem = true;
        public static List<IAsmHook> _asmHooks = new();

        public static IReverseWrapper<ChangeText> _onTextRead;
        public static IReverseWrapper<GetGatherSpot> _onGatherSpot;
        public static IReverseWrapper<CollectGatherSpot> _onCollectionGatherSpot;


        public static IFunction<AddItemToInventory> _addItemFunction;
        public static void SetUpFunctionHooks(IReloadedHooks hooks)
        {
            if (hooks == null) return;
            // Game functions
            //_addItemFunction = new Function<AddItemToInventory>((nuint)(Mod.ModuleBase + 0x0bdb90), hooks);
            _addItemFunction = hooks.CreateFunction<AddItemToInventory>((int)(Mod.ModuleBase + 0x0bdb90));
            Console.WriteLine("Test function location {0:X}", _addItemFunction.Address);
            //replacements
            string[] loadText =
            {
                "use32",
                $"{hooks.Utilities.GetAbsoluteCallMnemonics(OnChangeText, out _onTextRead)}",
                "pop ebp",
                "ret",
                //=
            };
            _asmHooks.Add(hooks.CreateAsmHook(loadText, (int)(Mod.ModuleBase + 0xDEA60), AsmHookBehaviour.ExecuteAfter).Activate());

            string[] loadGatheringSpot = {
                "use32",
                "mov edx,[esp+20]",
                $"{hooks.Utilities.GetAbsoluteCallMnemonics(OnGetGatherSpot, out _onGatherSpot)}",
            };
            _asmHooks.Add(hooks.CreateAsmHook(loadGatheringSpot, (int)(Mod.ModuleBase + 0xC32DE), AsmHookBehaviour.ExecuteFirst).Activate());

            string[] collectGatherSpot = {
                "use32",
                "push eax",
                "push edx",
                "mov edx,[ebp-0x228]",
                "mov eax,[ebp-0x22c]",
                $"{hooks.Utilities.GetAbsoluteCallMnemonics(OnCollectGatherSpot, out _onCollectionGatherSpot)}",
                "pop eax",
                "pop edx",
                "mov ecx,[ebp-0x04]",
            };
            _asmHooks.Add(hooks.CreateAsmHook(collectGatherSpot, (int)(Mod.ModuleBase + 0x20C59B), AsmHookBehaviour.DoNotExecuteOriginal).Activate());

        }
        //get dungeon and spot id to send it to the ap server
        [Function(new[] { FunctionAttribute.Register.eax,FunctionAttribute.Register.edx }, FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
        public delegate int GetGatherSpot(int dungeonID,int dungeoFlag);
        public static unsafe int OnGetGatherSpot(int eax,int edx)
        {
            Console.WriteLine($"Dungeon ID = {eax}, Gather Flag ID = {edx}");
            long GatherspotID = (eax * 10) + edx+1;
            Mod.APClient.SendLocation(GatherspotID);
            return eax;
        }

        [Function(CallingConventions.Stdcall)]
        public delegate int AddItemToInventory(uint itemID,uint qunatity,char dunno);
        //determines if an item need to be set in the players inventory
        [Function(new FunctionAttribute.Register[] { FunctionAttribute.Register.eax,FunctionAttribute.Register.edx }, FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
        public delegate int CollectGatherSpot(uint item,uint quantity);
        public static unsafe int OnCollectGatherSpot(uint eax,uint edx)
        {
            Console.WriteLine($"item id:{eax} quantity:{edx}");
            if (!IsAPItem)
                //non randomized item
                if(true)
                    _addItemFunction.GetWrapper()(eax,edx,(char)1);
                else
                    _addItemFunction.GetWrapper()(1, 1, (char)1);
            return (int)eax;
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
            return stringPointer;
        }


    }
}
