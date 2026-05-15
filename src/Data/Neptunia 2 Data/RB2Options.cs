using Newtonsoft.Json.Linq;

namespace Nep3ArchipelagoClient.Data.Neptunia_2_Data
{
    internal class RB2Options:APOptions
    {
        public int OldSwordCount;
        public override void ParseOptions(JObject options)
        {
            if (options.ContainsKey("goal_required"))
                OldSwordCount = (int)(long)options.GetValue("goal_required");
        }
    }
}
