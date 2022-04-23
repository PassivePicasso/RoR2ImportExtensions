using ThunderKit.Integrations.Thunderstore;

namespace RiskOfThunder.RoR2Importer
{
    public class InstallBepInEx : ThunderstorePackageInstaller
    {
        public override int Priority => Constants.Priority.InstallBepInEx;
        public override string SourcePath => "Assets/ThunderKitSettings/RoR2Thunderstore.asset";
        public override string DependencyId => "bbepis-BepInExPack";
        public override string Description => $"Installs the latest version of BepInEx.\r\nThe Unified BepInEx all-in-one modding pack - plugin framework, detour library";
        public override string Name => $"Install BepInEx";
    }
}

