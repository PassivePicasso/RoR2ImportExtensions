using System.IO;
using ThunderKit.Core.Config;


namespace RiskOfThunder.RoR2Importer
{
    public class RoR2AssemblyPatcher : ManagedAssemblyPatcher
    {
        public override string Name => "RoR2 Assembly Patcher";
        public override string Description => $"Patches the game's RoR2.dll to fix stability issues in the editor.";
        public override int Priority => Constants.Priority.LegacyResourceAPIPatcher;
        public override string AssemblyFileName => "RoR2.dll";
        public override string BsDiffPatchPath => Path.Combine("Packages", "riskofthunder-ror2importer", "BinaryDiff", "RoR2.diff");
    }
}