using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ThunderKit.Core.Config;
using ThunderKit.Core.Data;
using ThunderKit.Markdown;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace RiskOfThunder.RoR2Importer
{
    public class PublicizerDataStorer : OptionalExecutor
    {
        public const string NStripExePath = "Packages/bepinex-nstrip/NStrip.exe";
        public override string Name => "Assembly Publicizer";
        public override string Description => "Assemblies listed in here will be publicized using NStrip." +
            "\nPublicized assemblies retain their inspector look and functionality, this does not strip assemblies.";
        public override int Priority => ThunderKit.Common.Constants.ConfigPriority.AssemblyImport + 125_000;

        public List<string> assemblyNames = new List<string> { "RoR2.dll", "KinematicCharacterController.dll" };

        public UnityEngine.Object nStripExecutable;

        private SerializedObject serializedObject;
        private VisualElement rootVisualElement;
        private MarkdownElement MessageElement
        {
            get
            {
                if(_messageElement == null)
                {
                    _messageElement = new MarkdownElement();
                    _messageElement.MarkdownDataType = MarkdownDataType.Text;
                }
                return _messageElement;
            }
        }
        private MarkdownElement _messageElement;

        public override void Execute()
        { }

        protected override VisualElement CreateProperties()
        {
            serializedObject = new SerializedObject(this);
            var executableProperty = serializedObject.FindProperty(nameof(nStripExecutable));
            var assemblyList = serializedObject.FindProperty(nameof(assemblyNames));
            rootVisualElement = new VisualElement();

            //Nstrip should ideally be located automatically, This method should find it if nstrip is in Packages, which it should be.
            if (executableProperty.objectReferenceValue == null)
            {
                //If NStrip couldnt be located, display warning
                if(TryToFindNStripExecutable(out var executable))
                {
                    executableProperty.objectReferenceValue = executable;
                    serializedObject.ApplyModifiedProperties();
                }
                else
                {
                    MessageElement.Data = $"***__WARNING__***: Could not find NStrip Executable! Hover over the \"N Strip Executable\" field for instructions.";
                    rootVisualElement.Add(MessageElement);
                }
            }

            PropertyField nstripField = new PropertyField(executableProperty);
            nstripField.tooltip = $"The NStrip executable, this is used for the publicizing system" +
                $"\nIf this field appears to be empty, then the RoR2Importer has failed to find the executable automatically." +
                $"\nPlease select the NStrip executable in your project, if no Executable exists, download NStrip version 1.4 or newer";
            nstripField.RegisterCallback<ChangeEvent<UnityEngine.Object>>(OnNStripSet);
            rootVisualElement.Add(nstripField);

            PropertyField listField = new PropertyField(assemblyList);
            listField.tooltip = $"A list of assembly names to publicize, Case Sensitive.";
            rootVisualElement.Add(listField);
            return rootVisualElement;
        }

        private void OnNStripSet(ChangeEvent<UnityEngine.Object> evt)
        {
            var nstrip = evt.newValue;
            if (nstrip == null)
            {
                MessageElement.Data = $"***__WARNING__***: Could not find NStrip Executable! Hover over the \"N Strip Executable\" field for instructions.";
                if(!rootVisualElement.Contains(MessageElement))
                {
                    rootVisualElement.Add(MessageElement);
                }
                return;
            }

            var relativePath = AssetDatabase.GetAssetPath(nstrip);
            var fullPath = Path.GetFullPath(relativePath);
            var fileName = Path.GetFileName(fullPath);

            if (fileName != "NStrip.exe")
            {
                MessageElement.Data = $"Object in \"N Strip Executable\" is not NStrip!";
                if(!rootVisualElement.Contains(MessageElement))
                {
                    rootVisualElement.Add(MessageElement);
                }
                return;
            }

            MessageElement.RemoveFromHierarchy();
        }

        private bool TryToFindNStripExecutable(out UnityEngine.Object nstripExecutable)
        {
            var nstrip = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(NStripExePath);
            nstripExecutable = nstrip == null ? null : nstrip;
            return nstripExecutable != null;
        }

        internal static PublicizerDataStorer GetDataStorer()
        {
            var settings = ThunderKitSetting.GetOrCreateSettings<ImportConfiguration>();
            var publicizerDataStorer = settings.ConfigurationExecutors.OfType<PublicizerDataStorer>().FirstOrDefault();
            return publicizerDataStorer;
        }
    }
}