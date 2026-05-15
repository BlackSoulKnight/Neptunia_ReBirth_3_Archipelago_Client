
using Newtonsoft.Json.Linq;

namespace Nep3ArchipelagoClient.Data
{
    internal abstract class APOptions
    {
        public abstract void ParseOptions(JObject options);
    }
}
