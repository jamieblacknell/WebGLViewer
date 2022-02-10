using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using SFB;
using System.Runtime.InteropServices;

public class GetFilePathFromExplorer : MonoBehaviour
{

#if UNITY_WEBGL && !UNITY_EDITOR

    // WebGL

    [DllImport("__Internal")]
    private static extern void UploadFile(string gameObjectName, string methodName, string filter, bool multiple);

    public void OpenExplorer() {
        UploadFile(gameObject.name, "OnFileUpload", ".glb", false);
    }

    // Called from browser
    public void OnFileUpload(string url) {
        StartCoroutine(GetFile(url));
    }
#else
    
    public void OpenExplorer()
    {
        var paths = StandaloneFileBrowser.OpenFilePanel("Open GLB File", "", "glb", false);
        StartCoroutine(GetFile(paths[0]));
    }

#endif

    private IEnumerator GetFile(string path)
    {
        

        string newPath = path.Replace("\\", "/");

        Debug.Log(newPath);

        using (UnityWebRequest webRequest = UnityWebRequest.Get(newPath))
        {
           yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(webRequest.error);
            }
            else
            {
                PCAImporter.Instance.ImportPCAGLB(newPath);
            }
        }
    }
}


