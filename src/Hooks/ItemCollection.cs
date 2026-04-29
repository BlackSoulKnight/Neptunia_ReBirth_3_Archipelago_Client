using Nep3ArchipelagoClient;
using Nep3ArchipelagoClient.Archipelago;
using Reloaded.Hooks;
using Reloaded.Hooks.Definitions;
using Reloaded.Hooks.Definitions.Enums;
using Reloaded.Hooks.Definitions.Structs;
using Reloaded.Hooks.Definitions.X86;
using Reloaded.Memory;
using System.Diagnostics.Metrics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;
using System.Text;

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
        public static IReverseWrapper<GetDungeonTresureId> _onGetDungeonTresureId;
        public static IReverseWrapper<GetEnemyDropString> _onGetEnemyDropString;
        public static IReverseWrapper<OnNewEnemyKilled> _onNewEnemyKilled;

        public static void ClearReplacementText()
        {
            for (int i = 0; i < ReplacementText.Length; i++)
            {
                ReplacementText[i] = 0;
            }
        }

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

            string[] getTreasureId = {
                "use32",
                "pushad",
                "pushfd",
                $"{hooks.Utilities.GetAbsoluteCallMnemonics(OnGetDungeonTresureId, out _onGetDungeonTresureId)}",
                "popfd",
                "popad",
            };
            _asmHooks.Add(hooks.CreateAsmHook(getTreasureId, (int)(Mod.ModuleBase + 0xB8D1D), AsmHookBehaviour.ExecuteFirst).Activate());

            string[] enemyDrop = {
                "use32",
                "pushad",
                "pushfd",
                $"{hooks.Utilities.GetAbsoluteCallMnemonics(OnGetEnemyDropString, out _onGetEnemyDropString)}",
                "popfd",
                "popad",
            };
            _asmHooks.Add(hooks.CreateAsmHook(enemyDrop, (int)(Mod.ModuleBase + 0x174602), AsmHookBehaviour.ExecuteFirst).Activate());

            string[] enemyKilled = {
                "use32",
                "pushad",
                "pushfd",
                $"{hooks.Utilities.GetAbsoluteCallMnemonics(SendNewEnemyKilleCheck, out _onNewEnemyKilled)}",
                "popfd",
                "popad",
            };
            _asmHooks.Add(hooks.CreateAsmHook(enemyKilled, (int)(Mod.ModuleBase + 0xC0280), AsmHookBehaviour.ExecuteFirst).Activate());

        }
        //enemy Drops
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
        //get dungeon and spot id to send it to the ap server
        [Function(new[] { FunctionAttribute.Register.eax,FunctionAttribute.Register.edx }, FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
        public delegate int GetGatherSpot(int dungeonID,int dungeoFlag);
        public static unsafe int OnGetGatherSpot(int eax,int edx)
        {
            Console.WriteLine($"Dungeon ID = {eax}, Gather Flag ID = {edx}");
            long GatherspotID = (eax * 10) + edx+1;
            Mod.APClient.SendLocation(GatherspotID);
            Mod.APClient.GetItemName(GatherspotID, ref ReplacementText);
            DoReplaceText = true;
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
            APClient.collectedFirstItem = true;
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
            DoReplaceText = false;
            return stringPointer;
        }

        [Function(new[] { FunctionAttribute.Register.eax, FunctionAttribute.Register.ecx }, FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
        public delegate int GetDungeonTresureId(int eax,int dungeonTreasureID);
        public static unsafe int OnGetDungeonTresureId(int eax,int ecx)
        {
            Console.WriteLine($"Dungeon Tresure ID:{ecx}");
            var DungeonTreasureId = APClient.DungeonBaseID+ecx;
            DoReplaceText = true; 
            Mod.APClient.SendLocation(DungeonTreasureId);
            Mod.APClient.GetItemName(DungeonTreasureId, ref ReplacementText);

            return eax;
        }

        [Function(new[] { FunctionAttribute.Register.eax, FunctionAttribute.Register.esi }, FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
        public delegate int OnNewEnemyKilled(int eax, int enemyID);
        public static int SendNewEnemyKilleCheck(int eax, int esi)
        {
            var enemyId = esi & 0xFF;
            var EnemyId = APClient.EnemyBaseID + enemyId;
            Console.WriteLine($"Killed new enemy with the ID:{enemyId}");
            Mod.APClient.SendLocation(EnemyId);
            return eax;
        }

    }
}
