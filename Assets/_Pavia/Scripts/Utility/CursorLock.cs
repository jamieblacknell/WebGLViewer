using UnityEngine;

namespace Pavia.Utility
{
    public class CursorLock : MonoBehaviour
    {
        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

}