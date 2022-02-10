using UnityEngine;

namespace Pavia.Utility
{

    public class DDOL : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }

}