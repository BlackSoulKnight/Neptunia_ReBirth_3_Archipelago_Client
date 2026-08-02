using Nep3ArchipelagoClient.Archipelago;
using Nep3ArchipelagoClient.MemoryInterface;
using Reloaded.Hooks.Definitions;
using Reloaded.Hooks.Definitions.Enums;
using Reloaded.Hooks.Definitions.X86;
using Reloaded.Memory;
using System.Text;


namespace Nep3ArchipelagoClient.Hooks.Rebirth3
{
    internal class RB3EventHooks
    {
        public static List<IAsmHook> _asmHooks = new();

        public static IReverseWrapper<EventLoad> _onLoadDungeonEvent;

        [Function(new[] { FunctionAttribute.Register.eax,FunctionAttribute.Register.edx }, FunctionAttribute.Register.eax, FunctionAttribute.StackCleanup.Callee)]
        public delegate int EventLoad(int eventID,int origin);
        public static unsafe int OnEventDungeonLoad(int eax,int edx) {
            switch (edx)
            {
                case 0:
                    Console.WriteLine("Inside Dungeon");
                    break;
                case 1:
                    Console.WriteLine("From the Map");
                    break;
                case 2:
                    Console.WriteLine("Load Dungeon");
                    break;
                case 3:
                    Console.WriteLine("After Event");
                    break;
            }

            var eventId = eax;
            Console.WriteLine($"Check Event ID:{eventId}");
            switch (eventId)
            {
                case 1013:
                    if (Mod._configuration.SkipRei)
                    {
                        eventId = 3013;
                        Mod.SaveGame.SetEventFlag(1013, true);
                    }
                    break;
                case 1014:
                    if(Mod._configuration.SkipRei)
                        eventId = 3014;
                    break;
            }
            Console.WriteLine($"Change Event ID to {eventId}");

            return eventId;
        }

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
                "mov eax,[esp+0x24]",
                "mov edx,0",
                $"{hooks.Utilities.GetAbsoluteCallMnemonics(OnEventDungeonLoad, out _onLoadDungeonEvent)}",
                "mov [esp+0x24],eax",
                "popfd",
                "popad",
            };
            if (FunctionScanner.FindFunction("Inside Dungeon Event", "E8 ?? ?? ?? ?? 8B 8E ?? ?? ?? ?? 83 C4 04 8A F8", out offset))
                _asmHooks.Add(hooks.CreateAsmHook(Event, (int)(Mod.ModuleBase + offset), AsmHookBehaviour.ExecuteFirst).Activate());

            Event[4] = "mov edx,1";
            if (FunctionScanner.FindFunctions("Load City Event", "E8 ?? ?? ?? ?? 83 C4 04 84 C0 74 ?? 83 EC 0C C7 44 24 ?? 00 00 80 3F C7 44 24 ?? 00 00 80 3F C7 04 24 00 00 80 3F E8 ?? ?? ?? ?? 56 E8 ?? ?? ?? ?? 56 E8 ?? ?? ?? ?? 83 C4 14 C7 86 ?? ?? ?? ?? ?? ?? ?? ?? 33 C0 5E 5D C3 C7 86 ?? ?? ?? ?? ?? ?? ?? ?? 33 C0 5E 5D C3 ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ??", out offsets))
                foreach (var off in offsets)
                    _asmHooks.Add(hooks.CreateAsmHook(Event, (int)(Mod.ModuleBase + off), AsmHookBehaviour.ExecuteFirst).Activate());

            Event[4] = "mov edx,2";
            if (FunctionScanner.FindFunction("Load Dungeon Event", "E8 ?? ?? ?? ?? 83 C4 04 84 C0 74 ?? 8B 45 ?? C7 00 03 00 00 00", out offset))
                _asmHooks.Add(hooks.CreateAsmHook(Event, (int)(Mod.ModuleBase + offset), AsmHookBehaviour.ExecuteFirst).Activate());

            Event[4] = "mov edx,3";
            if (FunctionScanner.FindFunction("Event to Event", "E8 ?? ?? ?? ?? 83 C4 0C 84 C0 75 ?? BE 11 00 00 00", out offset))
                _asmHooks.Add(hooks.CreateAsmHook(Event, (int)(Mod.ModuleBase + offset), AsmHookBehaviour.ExecuteFirst).Activate());

        }

    }
}
