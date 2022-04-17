using System.IO;
using ThunderKit.Core.Config;
using UObject = UnityEngine.Object;

namespace RiskOfThunder.RoR2Importer
{
    public class PublicizerAssemblyProcessor : AssemblyProcessor
    {
        public override int Priority => 500;
        public override string Name => $"Assembly Publicizer";

        public override string Process(string path)
        {
            PublicizerDataStorer dataStorer = PublicizerDataStorer.GetDataStorer();
            //Publicizer not enabled? dont publicize
            if(!dataStorer.enabled)
                return path;

            var assemblyFileName = Path.GetFileName(path);
            //assembly file name is not in assemblyNames? dont publicize.
            if (!dataStorer.assemblyNames.Contains(assemblyFileName))
                return path;

            return path;
        }
    }
}