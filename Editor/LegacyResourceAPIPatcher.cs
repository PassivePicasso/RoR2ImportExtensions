using System.Collections;
using System.IO;
using System.Threading.Tasks;
using ThunderKit.Common;
using ThunderKit.Core.Config;
using ThunderKit.Core.Data;
using ThunderKit.Core.Utilities;
using UnityEditor;
using UnityEngine;
#if UNITY_2019
#elif UNITY_2018
using UnityEngine.Experimental.UIElements;
#endif


namespace RiskOfThunder.RoR2Importer
{
    public class LegacyResourceAPIPatcher : OptionalExecutor
    {
        public override string Name => "RoR2 LegacyResourceAPI Patcher";

        public override string Description => $"Patches the game's LegacyResourcesAPI.dll to improve stability and reduce editor hangs.";
        public override int Priority => Constants.Priority.LegacyResourceAPIPatcher;

        public override void Execute()
        {
            var settings = ThunderKitSetting.GetOrCreateSettings<ThunderKitSettings>();
            var legacyResourceAPIOrig = Path.GetFullPath(Path.Combine(settings.ManagedAssembliesPath, "LegacyResourcesAPI.dll"));
            var legacyResourceAPINew = Path.GetFullPath(Path.Combine(settings.PackageFilePath, "LegacyResourcesAPI.dll"));
            var diffPath = Path.GetFullPath(Path.Combine("Packages", "riskofthunder-ror2importer", "BinaryDiff", "LegacyResourcesAPI.diff"));
            var packagePath = Path.GetDirectoryName(legacyResourceAPINew);

            if (File.Exists(legacyResourceAPINew))
                File.Delete(legacyResourceAPINew);

            Directory.CreateDirectory(packagePath);

            BsDiff.BsTool.Patch(legacyResourceAPIOrig, legacyResourceAPINew, diffPath);

            var asmPath = legacyResourceAPINew.Replace("\\", "/");
            string assemblyFileName = Path.GetFileName(asmPath);
            var destinationMetaData = Path.Combine(settings.PackageFilePath, $"{assemblyFileName}.meta");
            PackageHelper.WriteAssemblyMetaData(asmPath, destinationMetaData);

            var escape = false;
            while (EditorApplication.isUpdating && !escape)
            {
                var x = escape;
            }
        }
    }
}