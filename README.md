
# RoR2 Import Extensions

RoR2 Import Extensions is a *thunderkit extension* aiming to reduce the time it takes to properly set up a RoR2 Thunderkit Project.

It is extremely recommended to install this and use it in the process of setting up a project, RoR2ImportExtensions can also be used as a use case example on how to extend the Thunderkit Importing Process.
### Extensions:
Note: Higher priority means it runs earlier

| Extension Name | Priority | Effect | Level of Recomendation |
|--|--|--|--|
| PostProcessing Package Installer | 3.25M | Installs the PostProcessing package version 2.3.0 and prevents the game's PostProcessing DLL from being imported | Recommended if working with PP
|TextMeshPro Uninstaller|3.24M|Removes Unity TextMeshPro due to compatibility issues with the games modified TextMeshPro library and ensures that Unity.TextMeshPro.dll is copied from the games directory|Highly Recommended|
|Unity GUI Uninstaller|3.23M|Removes Unity GUI due to compatibility issues with the game's modified TextMeshPro library and ensures that the Unity.UI.dll is copied from the games directory|Highly Recommended|
|Assembly Publicizer|3.125M|Publicizes the listed assemblies with N-Strip, publicized assemblies retain their editor functionality and inspector look| Recommended if publicizing is needed|
|RoR2 LegacyResourceAPI Patcher|2.75M|Patches the game's LegacyResourcesAPI to improve editor functionality and greatly reduce hangs/freezes|Extremely Recommended|
|Configure Addressable Graphics Settings|-5.01K|Assigns the Risk of Rain 2 DeferredShading and DeferredReflectionCustom shaders in the Addressable Graphics settings and by proxy in the Project's Graphics Settings|Recommended
Ensure RoR2 Thunderstore Source|-125k|Ensures the creation of a thunderstore source that points to https://thunderstore.io|Recommended
Install BepInExPack|-135K|Installs the latest version of BepInExPack|Extremely Recommended
Install FixPluginTypesSerialization|-140K|Installs the latest version of FixPluginTypesSerialization|Highly Recommended
|Install R2API|-145K|Installs the latest version of R2API|Optional but Recommended|
|Install RoR2MultiplayerHLAPI|-155K|Installs the latest version of the RoR2MultiplayerHLAPI and prevents the game's com.unity.mulitplayerhlapi.runtime.dll from being imported|Extremely Recommended
|Install RoR2EditorKit|-160K|Installs the latest version of RoR2EditorKit| Optional, but recommended|

Note: FixPluginTypesSerialization will soon be added to the RoR2BepInExPack and as such it's import extension will be removed in a later update

## Planned Features
An importer to automatically generate an MMHook for RoR2.dll

## Changelog

### `1.1.0`

- Add SetDeferredShading to fix Graphics settings not collected by Import Project settings

### '1.0.2'

- Add importer to fix graphics settings, enabling consistent rendering of shaders in the editor

### '1.0.1'

- Fix legacy api patcher path by @PassivePicasso in #3

### '1.0.0'

- Initial Release