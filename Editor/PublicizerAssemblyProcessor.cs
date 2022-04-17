using ThunderKit.Core.Config;

namespace RiskOfThunder.RoR2Importer
{
    public class PublicizerAssemblyProcessor : AssemblyProcessor
    {
        public override int Priority => -10;
        public override string Name => $"Assembly Publicizer";

        public override string Process(string path)
        {
            return path;
        }
    }
}