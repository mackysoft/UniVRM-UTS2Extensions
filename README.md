# UniVRM UTS2 (UnityChanToonShaderVer2) Extensions

This  package is to allow UniVRM to export / import UTS2 shader.

## Installation

### 1. Install UniVRM and UTS2

Install UniVRM and UTS2 in any way you like.

- UniVRM: https://github.com/vrm-c/UniVRM
- UTS2: https://github.com/unity3d-jp/UnityChanToonShaderVer2_Project

### 2. Install this package
https://github.com/mackysoft/UniVRM-UTS2Exporter/releases

### 3. Add `"Universal Render Pipeline/Toon"` to `VRMExtensionShaders in VRMShaders/VRM/IO/Runtime/PreShaderPropExporter.cs`

```cs
public static readonly string[] VRMExtensionShaders = new string[]
{
    "VRM/UnlitTransparentZWrite",
    "VRM/MToon",
    "Universal Render Pipeline/Toon" // Add this
};
```

Now you can export and import UTS2 with UniVRM.
