namespace RiskOfThunder.RoR2Importer
{
    using static ThunderKit.Common.Constants;
    public static class Constants
    {
        public static class Priority
        {
            public const int PostProcessingInstaller = ConfigPriority.AssemblyImport + 250_000;
            public const int TextMeshProUninstaller = ConfigPriority.AssemblyImport + 240_000;
            public const int AssemblyPublicizerConfiguration = ConfigPriority.AssemblyImport + 125_000;
            public const int LegacyResourceAPIPatcher = ConfigPriority.AssemblyImport - 250_000;
            public const int EnsureRoR2Thunderstore = ConfigPriority.AddressableCatalog - 125_000;
            public const int InstallBepInEx = ConfigPriority.AddressableCatalog - 135_000;
            public const int InstallR2API = ConfigPriority.AddressableCatalog - 145_000;
            public const int InstallMLAPI = ConfigPriority.AddressableCatalog - 155_000;
            public const int InstallRoR2EK = ConfigPriority.AddressableCatalog - 160_000;
            public const int ThunderstorePackageInstaller = ConfigPriority.AddressableCatalog - 250_000;
        }
    }
}