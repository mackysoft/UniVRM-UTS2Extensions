# UniVRM UTS2 (UnityChanToonShaderVer2) Exporter

https://github.com/unity3d-jp/UnityChanToonShaderVer2_Project

## Installation

### 1. Install UniVRM and UTS2


### 2. Install this package


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