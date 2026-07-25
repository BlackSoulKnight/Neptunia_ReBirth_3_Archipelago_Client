using Reloaded.Hooks.Definitions;


namespace Nep3ArchipelagoClient.Hooks.Rebirth3
{
    public class Hooks
    {
        public static void SetupHooks(IReloadedHooks hooks)
        {
            RB3ItemCollectionHooks.SetupHooks(hooks);
            RB3TextHooks.SetupHooks(hooks);
            RB3EventHooks.SetupHooks(hooks);
            RB3QuestHooks.SetupHooks(hooks);

            RB3DebugTools.SetupHooks(hooks);
        }
    }
}
