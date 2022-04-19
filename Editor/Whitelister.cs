using System.Collections.Generic;
using System.Linq;
using ThunderKit.Core.Config;

namespace RiskOfThunder.RoR2Importer
{
    public class Whitelister : WhitelistProcessor
    {
        public override string Name => "RoR2 Assembly Whitelist";

        public override int Priority => 750;

        public override IEnumerable<string> Process(IEnumerable<string> whitelist)
        {
            return whitelist.Append("Unity.TextMeshPro.dll");
        }
    }
}