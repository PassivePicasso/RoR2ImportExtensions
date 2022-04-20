using System.IO;
using ThunderKit.Core.Config;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
#if UNITY_2019
#elif UNITY_2018
using UnityEngine.Experimental.UIElements;
#endif


namespace RiskOfThunder.RoR2Importer
{
    public class RoR2UnityPackageInstaller : OptionalExecutor
    {
        public override string Name => "RoR2 Unity Package Installer";

        public override string Description => 
            $"Installs Unity PostProcessing Version 2.3.0" +
            $"\nRemoves Unity TextMeshPro";
        public override int Priority => Constants.Priority.UnityPackageInstaller;

        public override void Execute()
        {
            Request result = Client.Add("com.unity.postprocessing@2.3.0");
            var escape = false;
            while (!result.IsCompleted && !escape)
            {
                var x = escape;
            }

            Debug.Log("Installed com.unity.postprocessing@2.3.0");

            escape = false;
            while (EditorApplication.isUpdating && !escape)
            {
                var x = escape;
            }

            result = Client.Remove("com.unity.textmeshpro");
            escape = false;
            while (!result.IsCompleted && !escape)
            {
                var x = escape;
            }
            Debug.Log("Removed com.unity.textmeshpro");

            var manifestPath = Path.Combine("Packages", "manifest.json");
        }
    }
}