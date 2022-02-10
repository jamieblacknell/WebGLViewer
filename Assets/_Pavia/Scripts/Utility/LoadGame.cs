using UnityEngine;
using UnityEngine.SceneManagement;

namespace Pavia.Utility
{

    public class LoadGame : MonoBehaviour
    {
        private void Awake()
        {
            SceneManager.LoadScene("Viewer");
        }
    }

}