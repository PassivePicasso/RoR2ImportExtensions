using System.IO;
using ThunderKit.Core.Config;


namespace RiskOfThunder.RoR2Importer
{
    public class LegacyResourceAPIPatcher : ManagedAssemblyPatcher
    {
        public override string Name => "RoR2 LegacyResourceAPI Patcher";
        public override string Description => $"Patches the game's LegacyResourcesAPI.dll to improve stability and reduce editor hangs.";
        public override int Priority => Constants.Priority.LegacyResourceAPIPatcher;
        public override string AssemblyFileName => "LegacyResourcesAPI.dll";
        public override string BsDiffPatchPath => Path.Combine("Packages", "riskofthunder-ror2importextensions", "BinaryDiff", "LegacyResourcesAPI.diff");
    }
}