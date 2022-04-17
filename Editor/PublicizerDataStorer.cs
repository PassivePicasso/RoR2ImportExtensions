using System.Collections.Generic;
using ThunderKit.Core.Config;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace RiskOfThunder.RoR2Importer
{
    public class PublicizerDataStorer : OptionalExecutor
    {
        public override string Name => "Assembly Publicizer";
        public override string Description => "Assemblies listed in here will be publicized using NStrip." +
            "\nPublicized assemblies retain their inspector look and functionality, this does not strip assemblies.";
        public override int Priority => ThunderKit.Common.Constants.ConfigPriority.AssemblyImport + 125_000;

        public List<string> assemblyNames = new List<string> { "RoR2.dll" };

        public override void Execute()
        { }

        protected override VisualElement CreateProperties()
        {
            SerializedObject obj = new SerializedObject(this);
            PropertyField propField = new PropertyField(obj.FindProperty(nameof(assemblyNames)));
            return propField;
        }
    }
}