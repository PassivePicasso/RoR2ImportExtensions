using System;
using System.Collections.Generic;
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
        public override int Priority => ThunderKit.Common.Constants.ConfigPriority.AddressableCatalog - 50_000;
        public override string Description => $"Thunderstore related import options for RoR2";
        public override string Name => $"Thunderstore Package Installer";

        public bool createRoR2Source = true;

        public bool installModsFromRoR2Source = true;
        public List<string> ror2ModGUIDs = new List<string> { };

        public bool installThunderKitExtensions = true;
        public List<string> editorExtensionGUIDs = new List<string> { };

        private VisualElement rootElement;

        [SerializeField]
        private ThunderstoreSource ror2Source;

        protected override VisualElement CreateProperties()
        {
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
            }

            if(createRoR2Source && installModsFromRoR2Source && ror2ModGUIDs.Count > 0)
            {
                InstallModsFromRoR2Source();
            }

            if(installThunderKitExtensions && editorExtensionGUIDs.Count > 0)
            {
                InstallThunderKitExtensions();
            }
        }

        private void CreateRoR2Source()
        {
            ror2Source = CreateInstance<ThunderstoreSource>();
            ror2Source.Url = "https://thunderstore.io";
            AssetDatabase.CreateAsset(ror2Source, "Assets/ThunderKitSettings/RoR2Thunderstore.asset");
            PackageSource.LoadAllSources();
        }

        private void InstallModsFromRoR2Source()
        {

        }

        private void InstallThunderKitExtensions()
        {

        }
    }
}
