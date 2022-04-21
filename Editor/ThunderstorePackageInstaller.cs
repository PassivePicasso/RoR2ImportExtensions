using System;
using System.ComponentModel;
using System.Linq;
using ThunderKit.Common;
using ThunderKit.Core.Config;
using ThunderKit.Core.Data;
using ThunderKit.Core.Utilities;
using ThunderKit.Integrations.Thunderstore;
using UnityEditor;
using UnityEngine;

namespace RiskOfThunder.RoR2Importer
{
    public class ThunderstorePackageInstaller : OptionalExecutor
    {
        [Flags]
        public enum RoR2ThunderstorePackages
        {
            Nothing = 0,
            [Description("bbepis-BepInExPack")]
            BepInEx = 1,
            [Description("tristanmcpherson-R2API")]
            R2API = 2
        }

        [Flags]
        public enum RoR2ThunderKitExtensions
        {
            Nothing = 0,
            [Description("RiskofThunder-RoR2EditorKit")]
            RoR2EditorKit = 1,
            [Description("RiskofThunder-RoR2MultiplayerHLAPI")]
            RoR2MultiplayerHLAPI = 2
        }

        public override int Priority => Constants.Priority.ThunderstorePackageInstaller;
        public override string Description => $"Thunderstore related import options for RoR2";
        public override string Name => $"Thunderstore Package Installer";
        protected override string UITemplatePath => "Packages/riskofthunder-ror2importer/UXML/ThunderstorePackageInstaller.uxml";

        public bool CreateRoR2Source = true;
        public RoR2ThunderstorePackages RoR2Dependencies = RoR2ThunderstorePackages.R2API | RoR2ThunderstorePackages.BepInEx;
        public RoR2ThunderKitExtensions ThunderKitExtensions = RoR2ThunderKitExtensions.RoR2EditorKit | RoR2ThunderKitExtensions.RoR2MultiplayerHLAPI;

        [SerializeField]
        private ThunderstoreSource ror2Source;

        public override void Execute()
        {
            if (CreateRoR2Source)
            {
                if (!ror2Source) GetOrCreateRoR2Source();

                PackageSource.LoadAllSources();
            }
            if (ror2Source)
                InstallModsFromRoR2Source();

            InstallThunderKitExtensions();

            PackageHelper.ResolvePackages();
        }

        private void GetOrCreateRoR2Source()
        {
            var pss = ThunderKitSetting.GetOrCreateSettings<PackageSourceSettings>();
            foreach (var source in pss.PackageSources)
            {
                if (source is ThunderstoreSource tsSource)
                {
                    if (tsSource.Url == "https://thunderstore.io")
                    {
                        ror2Source = tsSource;
                        return;
                    }
                }
            }

            Debug.Log($"Creating RoR2 Thunderstore Source");
            ror2Source = CreateInstance<ThunderstoreSource>();
            ror2Source.Url = "https://thunderstore.io";
            AssetDatabase.CreateAsset(ror2Source, "Assets/ThunderKitSettings/RoR2Thunderstore.asset");
        }

        private void InstallModsFromRoR2Source()
        {
            try
            {
                EditorApplication.LockReloadAssemblies();
                foreach (RoR2ThunderstorePackages dependency in Enum.GetValues(typeof(RoR2ThunderstorePackages)))
                {
                    if (!RoR2Dependencies.HasFlag(dependency))
                        continue;

                    string valueName = dependency.GetDescription();

                    var pss = ThunderKitSetting.GetOrCreateSettings<PackageSourceSettings>();
                    foreach (var source in pss.PackageSources)
                    {
                        var package = source.Packages.FirstOrDefault(pkg => pkg.DependencyId == valueName);
                        if (package == null)
                        {
                            Debug.LogWarning($"Could not find package with DependencyId of {valueName}");
                            continue;
                        }

                        if (package.Installed)
                        {
                            Debug.LogWarning($"Not installing package with DependencyId of {valueName} because it's already installed");
                            continue;
                        }

                        Debug.Log($"Installing latest version of package {valueName});");
                        var task = source.InstallPackage(package, "latest");
                        while(!task.IsCompleted)
                        {
                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
            }
            finally
            {
                EditorApplication.UnlockReloadAssemblies();
            }
        }

        private void InstallThunderKitExtensions()
        {
            try
            {
                EditorApplication.LockReloadAssemblies();
                foreach (RoR2ThunderKitExtensions tsPackage in Enum.GetValues(typeof(RoR2ThunderKitExtensions)))
                {
                    if (!ThunderKitExtensions.HasFlag(tsPackage))
                        continue;

                    string valName = tsPackage.GetDescription();

                    var pss = ThunderKitSetting.GetOrCreateSettings<PackageSourceSettings>();
                    foreach (var source in pss.PackageSources)
                    {
                        var package = source.Packages.FirstOrDefault(pkg => pkg.DependencyId == valName);
                        if (package == null)
                        {
                            Debug.LogWarning($"Could not find package with DependencyId of {valName}");
                            continue;
                        }

                        if (package.Installed)
                        {
                            Debug.LogWarning($"Not installing package with DependencyId of {valName} because it's already installed");
                            continue;
                        }

                        Debug.Log($"Installing latest version of package {valName});");
                        var task = source.InstallPackage(package, "latest");
                        while (!task.IsCompleted)
                        {

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
            }
            finally
            {
                EditorApplication.UnlockReloadAssemblies();
            }
        }
    }
}

