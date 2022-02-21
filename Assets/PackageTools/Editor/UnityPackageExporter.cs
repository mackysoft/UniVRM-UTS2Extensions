using System.IO;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace MackySoft.PackageTools.Editor {

	public static class UnityPackageExporter {

		// The name of the unitypackage to output.
		const string k_PackageName = "UniVRM-UTS2Extensions";

		// The path to the package under the `Assets/` folder.
		const string k_PackagePath = "MackySoft";

		// Path to export to.
		const string k_ExportPath = "Build";

		const string k_SearchPattern = "*";
		const string k_PackageToolsFolderName = "PackageTools";

		[MenuItem("Tools/UniVRM-UTS2Extensions/Export Package")]
		public static void Export () {
			ExportPackage($"{k_ExportPath}/{k_PackageName}.unitypackage",GetAssetPaths());
			ExportPackage($"{k_ExportPath}/VRMShaderUpdate.unitypackage",GetVRMShaderUpdateAssetPaths());
		}

		public static string ExportPackage (string exportPath,string[] assetPaths) {
			// Ensure export path.
			var dir = new FileInfo(exportPath).Directory;
			if (dir != null && !dir.Exists) {
				dir.Create();
			}

			// Export
			AssetDatabase.ExportPackage(
				assetPaths,
				exportPath,
				ExportPackageOptions.Default
			);

			return Path.GetFullPath(exportPath);
		}

		public static string[] GetAssetPaths () {
			var path = Path.Combine(Application.dataPath,k_PackagePath);
			var assets = Directory.EnumerateFiles(path,k_SearchPattern,SearchOption.AllDirectories)
				.Where(x => !x.Contains(k_PackageToolsFolderName))
				.Select(x => "Assets" + x.Replace(Application.dataPath,"").Replace(@"\","/"))
				.ToArray();
			return assets;
		}

		public static string[] GetVRMShaderUpdateAssetPaths () {
			return new string[] {
				"Assets/VRMShaders/VRM/IO/Runtime/PreShaderPropExporter.cs",
				"Assets/VRMShaders/VRM/IO/Runtime/PreShaderPropExporter.cs.meta"
			};
		}
	}
}