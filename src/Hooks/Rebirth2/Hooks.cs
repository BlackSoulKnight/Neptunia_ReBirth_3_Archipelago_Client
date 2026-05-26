using Reloaded.Hooks.Definitions;


namespace Nep3ArchipelagoClient.Hooks.Rebirth2
{
    public class Hooks
    {
        public static void SetupHooks(IReloadedHooks hooks)
        {
            RB2ItemCollectionHooks.SetupHooks(hooks);
            RB2TextHooks.SetupHooks(hooks);
            RB2EventHooks.SetupHooks(hooks);
#if DEBUG
            RB2DebugTools.SetupHooks(hooks);
#endif
        }
    }
}
