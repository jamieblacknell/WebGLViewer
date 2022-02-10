using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.IO;
using Siccity.GLTFUtility;
using Pavia.Core;

// IMPORTANT - Requires GLTF importer package found here: https://github.com/Siccity/GLTFUtility

namespace Pavia.GTLF
{
    public class ModelDownloadImport : MonoBehaviour
    {
        private const string MASTER_URL = "https://pavia-testing-glbs.s3.us-east-2.amazonaws.com/";
        private const string FILE_EXT = ".glb";

        private void Start()
        {
            DownloadAndImportModel(PlayerDataManager.Instance.GetComponent<InitialPlayerFormData>().PcaName);
        }

        public void DownloadAndImportModel(string name)
        {
            if (File.Exists(Application.persistentDataPath + "/" + name + FILE_EXT))
            {
                Debug.Log("File found locally, importing from disk");
                ImportGLModel(name);
                return;
            }

            StartCoroutine(DownloadFile(name));
        }

        private IEnumerator DownloadFile(string name)
        {
            string url = MASTER_URL + name + FILE_EXT;
            url.Replace(" ", "%20");

            var webRequest = new UnityWebRequest(url, UnityWebRequest.kHttpVerbGET);
            string localPath = Path.Combine(Application.persistentDataPath, name + FILE_EXT);
            webRequest.downloadHandler = new DownloadHandlerFile(localPath);

            Debug.Log("Importer Path: " + localPath);

            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(webRequest.error);
            }
            else
            {
                Debug.Log("File successfully downloaded and saved to " + localPath + " (" + webRequest.downloadedBytes / 1024 + "kb)");
                ImportGLModel(name);
            }
        }

        private void ImportGLModel(string name)
        {
            string path = Application.persistentDataPath + "/" + name + FILE_EXT;

            // FINAL OUTPUT GAMEOBJECT TO DO WHAT YOU WANT WITH
            GameObject importedGameObject = Importer.LoadFromFile(path);

            SetupLandParcel(importedGameObject);

        }

        private void SetupLandParcel(GameObject pcaRoot)
        {
            pcaRoot.transform.SetParent(this.gameObject.transform);
            pcaRoot.transform.localPosition = Vector3.zero;

            

        }
    }

}