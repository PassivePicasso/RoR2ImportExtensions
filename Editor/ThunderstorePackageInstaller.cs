using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThunderKit.Common;
using ThunderKit.Core.Config;
using ThunderKit.Core.Data;
using ThunderKit.Core.UIElements;
using ThunderKit.Integrations.Thunderstore;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace RiskOfThunder.RoR2Importer
{
    public class ThunderstorePackageInstaller : OptionalExecutor
    {
        [Flags]
        public enum CommonRoR2Dependencies
        {
            [Description("bepis-BepInExPack")]
            BepInEx,
            [Description("tristanmcpherson-R2API")]
            R2API
        }

        [Flags]
        public enum CommonRoR2ThunderKitExtensions
        {
            [Description("RiskofThunder-RoR2EditorKit")]
            RoR2EditorKit = 1,
            [Description("RiskofThunder-RoR2MultiplayerHLAPI")]
            RoR2MultiplayerHLAPI = 2
        }

        public override int Priority => ThunderKit.Common.Constants.ConfigPriority.AddressableCatalog - 50_000;
        public override string Description => $"Thunderstore related import options for RoR2";
        public override string Name => $"Thunderstore Package Installer";

        public bool createRoR2Source = true;
        public CommonRoR2Dependencies ror2Dependencies = CommonRoR2Dependencies.R2API | CommonRoR2Dependencies.BepInEx;

        public CommonRoR2ThunderKitExtensions thunderKitExtensions = CommonRoR2ThunderKitExtensions.RoR2EditorKit | CommonRoR2ThunderKitExtensions.RoR2MultiplayerHLAPI;

        private VisualElement rootElement;

        [SerializeField]
        private ThunderstoreSource ror2Source;

        protected override VisualElement CreateProperties()
        {
            rootElement = new VisualElement();
            rootElement = TemplateHelpers.LoadTemplateInstance("Packages/riskofthunder-ror2importer/Editor/ThunderstorePackageInstaller.uxml", rootElement);
            rootElement.AddEnvironmentAwareSheets(Constants.ThunderKitSettingsTemplatePath);
            return rootElement;
        }

        public override void Execute()
        {
            if(createRoR2Source)
            {
                if(ror2Source == null)
                    CreateRoR2Source();
                PackageSource.LoadAllSources();
                InstallModsFromRoR2Source();
            }

            InstallThunderKitExtensions();
        }

        private void CreateRoR2Source()
        {
            ror2Source = CreateInstance<ThunderstoreSource>();
            ror2Source.Url = "https://thunderstore.io";
            AssetDatabase.CreateAsset(ror2Source, "Assets/ThunderKitSettings/RoR2Thunderstore.asset");
            PackageSource.LoadAllSources();
        }

        private async void InstallModsFromRoR2Source()
        {
            foreach(CommonRoR2Dependencies dependency in Enum.GetValues(typeof(CommonRoR2Dependencies)))
            {
                if (!ror2Dependencies.HasFlag(dependency))
                    continue;

                string valueName = dependency.GetDescription();

                var pss = ThunderKitSetting.GetOrCreateSettings<PackageSourceSettings>();
                foreach(var source in pss.PackageSources)
                {
                    var package = source.Packages.FirstOrDefault(pkg => pkg.DependencyId == valueName);
                    if (package == null)
                        continue;

                    if (package.Installed)
                        continue;

                    await source.InstallPackage(package, "latest");
                }
            }
        }

        private async void InstallThunderKitExtensions()
        {
            foreach(CommonRoR2ThunderKitExtensions extension in Enum.GetValues(typeof(CommonRoR2ThunderKitExtensions)))
            {
                if (!thunderKitExtensions.HasFlag(extension))
                    continue;

                string valueName = extension.GetDescription();

                var pss = ThunderKitSetting.GetOrCreateSettings<PackageSourceSettings>();
                foreach(var source in pss.PackageSources)
                {
                    var package = source.Packages.FirstOrDefault(pkg => pkg.DependencyId == valueName);
                    if (package == null)
                        continue;

                    if (package.Installed)
                        continue;

                    await source.InstallPackage(package, "latest");
                }
            }
        }
    }
}
