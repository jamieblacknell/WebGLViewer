using Siccity.GLTFUtility;
using UnityEngine;

public class PCAImporter : MonoBehaviour
{
    private const string TRANSPARENT_MESH_SUFFIX = "_t";
    private const string CLIP_MESH_SUFFIX = "_c";

    public static PCAImporter Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void ImportPCAGLB(string path)
    {
        GameObject currentPCA = GameObject.Find("Root");

        if (currentPCA != null)
        {
            Destroy(currentPCA);
        }

        GameObject importedGameObject = Importer.LoadFromFile(path);
        SetUpPCA(importedGameObject);
    }

    private void SetUpPCA(GameObject pcaRoot)
    {
        pcaRoot.name = "Root";
        pcaRoot.transform.SetParent(this.gameObject.transform);
        pcaRoot.transform.localPosition = Vector3.zero;

        Transform[] pcaChildren = pcaRoot.GetComponentsInChildren<Transform>();

        foreach (Transform pcaChild in pcaChildren)
        {
            string meshName = pcaChild.name;
            string suffix = meshName.Substring(meshName.Length - 2);

            if (suffix != CLIP_MESH_SUFFIX)
            {
                MeshCollider childMeshCollider = pcaChild.gameObject.AddComponent<MeshCollider>();
            }
        
            if (suffix == TRANSPARENT_MESH_SUFFIX)
            {
                pcaChild.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off; 
            }
        }
    }

}
