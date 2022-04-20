namespace RiskOfThunder.RoR2Importer
{
    using static ThunderKit.Common.Constants;
    public static class Constants
    {
        public static class Priority
        {
            public const int UnityPackageInstaller = ConfigPriority.AssemblyImport + 250_000;
            public const int ThunderstorePackageInstaller = ConfigPriority.AddressableCatalog - 250_000;
            public const int LegacyResourceAPIPatcher = ConfigPriority.AssemblyImport - 250_000;
            public const int AssemblyPublicizerConfiguration = ConfigPriority.AssemblyImport + 125_000;
        }
    }
}
