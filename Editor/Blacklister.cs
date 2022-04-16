using System.Collections.Generic;
using System.Linq;
using ThunderKit.Core.Config;

namespace RiskOfThunder.RoR2Importer
{
    public class Blacklister : BlacklistProcessor
    {
        public override string Name => "RoR2 Assembly Blacklist";

        public override int Priority => 0;

        public override IEnumerable<string> Process(IEnumerable<string> blacklist)
        {
            return blacklist
                .Append("Unity.Postprocessing.Runtime.dll")
                .Append("LegacyResourceAPI.dll")
                ;
        }
    }
}