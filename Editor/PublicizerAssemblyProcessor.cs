using System.IO;
using ThunderKit.Core.Config;
using UnityEngine;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using UObject = UnityEngine.Object;
using System.Threading.Tasks;
using UnityEditor;
using System.Collections.Generic;
using ThunderKit.Core.Data;
using ThunderKit.Core.Paths;
using System.Text;

namespace RiskOfThunder.RoR2Importer
{
    public class PublicizerAssemblyProcessor : AssemblyProcessor
    {
        public override int Priority => 500;
        public override string Name => $"Assembly Publicizer";
        public override string Process(string assemblyPath)
        {
            PublicizerDataStorer dataStorer = PublicizerDataStorer.GetDataStorer();
            //Publicizer not enabled? dont publicize
            if(!dataStorer.enabled)
                return assemblyPath;

            var assemblyFileName = Path.GetFileName(assemblyPath);
            //assembly file name is not in assemblyNames? dont publicize.
            if (!dataStorer.assemblyNames.Contains(assemblyFileName))
                return assemblyPath;

            UObject nstripExe = dataStorer.nStripExecutable;
            if (nstripExe == null)
            {
                Debug.LogWarning($"Could not strip assembly {assemblyFileName}, as NStrip has not been located.");
                return assemblyPath;
            }

            string ror2ManagedDir = ThunderKitSetting.GetOrCreateSettings<ThunderKitSettings>().ManagedAssembliesPath;

            string thunderkitRoot = Application.dataPath.Replace("Assets", "ThunderKit");
            string nstripFolder = Path.Combine(thunderkitRoot, "NStrip");
            if(!Directory.Exists(nstripFolder))
            {
                Directory.CreateDirectory(nstripFolder);
            }
            string outputPath = Path.Combine(nstripFolder, assemblyFileName);
            string nstripPath = Path.GetFullPath(AssetDatabase.GetAssetPath(nstripExe));

            List<string> arguments = new List<string>
            {
                "nstrip.exe",
                "-p",
                "-n",
                $"-d \"{ror2ManagedDir}\"",
                "-cg",
                "-cg-exclude-events",
                "-remove-readonly",
                "-unity-non-serialized",
                $"\"{assemblyPath}\"",
                $"\"{outputPath}\""
            };

            List<string> log = new List<string> { $"Publicized {assemblyFileName} with the following arguments:" };
            log.AddRange(StripAssembly(arguments, nstripPath));
            Debug.Log(string.Join("\n", log));

            return outputPath;
        }

        private List<string> StripAssembly(List<string> arguments, string nstripPath)
        {
            var args = new StringBuilder();
            var logger = new List<string>();
            for (int i = 0; i < arguments.Count; i++)
            {
                args.Append(arguments[i]);
                args.Append(" ");
                logger.Add($"Argument {i}: {arguments[i]}");
            }

            ProcessStartInfo psi = new ProcessStartInfo(nstripPath)
            {
                WorkingDirectory = Path.GetDirectoryName(nstripPath),
                Arguments = args.ToString(),
            };
            var process = System.Diagnostics.Process.Start(psi);
            process.WaitForExit(5000);
            return logger;
        }
    }
}