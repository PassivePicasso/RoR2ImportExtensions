using ThunderKit.Integrations.Thunderstore;

namespace RiskOfThunder.RoR2Importer
{
    public class InstallR2API : ThunderstorePackageInstaller
    {
        public override int Priority => Constants.Priority.InstallR2API;
        public override string SourcePath => "Assets/ThunderKitSettings/RoR2Thunderstore.asset";
        public override string DependencyId => "tristanmcpherson-R2API";
        public override string Description => $"Installs the R2API, a community supported common API for modding Risk of Rain 2";
        public override string Name => $"Install R2API";
    }
}

