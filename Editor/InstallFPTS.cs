using ThunderKit.Integrations.Thunderstore;

namespace RiskOfThunder.RoR2Importer
{
    public class InstallFPTS : ThunderstorePackageInstaller
    {
        public override int Priority => Constants.Priority.InstallFPTS;
        public override string ThunderstoreAddress => "https://thunderstore.io";
        public override string DependencyId => "RiskofThunder-FixPluginTypesSerialization";
        public override string Description => $"Installs FixPluginTypesSerialization: Fix custom Serializable structs/classes in assets from AssetBundles not properly getting deserialized by Unity.";
        public override string Name => $"Install FixPluginTypesSerialization";
    }
}

