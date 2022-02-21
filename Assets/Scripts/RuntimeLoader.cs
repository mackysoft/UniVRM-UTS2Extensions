using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UniGLTF;
using UnityEngine;
using UnityEngine.Networking;
using VRM;

public class RuntimeLoader : MonoBehaviour {

	void Start () {
		var request = UnityWebRequest.Get(Application.streamingAssetsPath + "/Alicia_solid.vrm");
		request.SendWebRequest().completed += op => ImportVRMAsync_Net4((UnityWebRequestAsyncOperation)op);
	}

	private async Task ImportVRMAsync_Net4 (UnityWebRequestAsyncOperation op) {
		

		//VRMImporterContext‚ªVRM‚ğ“Ç‚İ‚Ş‹@”\‚ğ’ñ‹Ÿ‚µ‚Ü‚·
		var gltf = new GlbLowLevelParser("",op.webRequest.downloadHandler.data).Parse();
		var data = new VRMData(gltf);
		var context = new VRMImporterContext(data);
		
		var meta = context.ReadMeta(false);
		Debug.LogFormat("meta: title:{0}",meta.Title);

		var instance = await context.LoadAsync();
		
		instance.Root.transform.SetParent(transform,false);
		instance.ShowMeshes();
	}
}