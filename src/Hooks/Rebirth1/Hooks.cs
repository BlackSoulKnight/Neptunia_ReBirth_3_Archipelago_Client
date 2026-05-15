using Nep3ArchipelagoClient.Hooks;
using Reloaded.Hooks.Definitions;


namespace Nep3ArchipelagoClient.Hooks.Rebirth1
{
    internal class Hooks
    {
        public static void SetupHooks(IReloadedHooks hooks)
        {
#if DEBUG
            RB1DebugTools.SetupHooks(hooks);
#endif
        }
    }
}
