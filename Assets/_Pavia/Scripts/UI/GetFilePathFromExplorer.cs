using System.Collections;
using UnityEngine;
using AnotherFileBrowser.Windows;
using UnityEngine.Networking;

public class GetFilePathFromExplorer : MonoBehaviour
{
    public void OpenExplorer()
    {
        var browerPropertes = new BrowserProperties();
        browerPropertes.filter = "GLB files (*.glb,) | *.glb;";
        browerPropertes.filterIndex = 0;

        new FileBrowser().OpenFileBrowser(browerPropertes, path =>
        {
            StartCoroutine(GetFile(path));
        });
    }

    private IEnumerator GetFile(string path)
    {
        string newPath = path.Replace("\\", "/");

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



