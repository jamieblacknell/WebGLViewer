using UnityEngine;

namespace Pavia.Core
{

    public class PlayerDataManager : MonoBehaviour
    {

        public static PlayerDataManager Instance;

        private void Awake()
        {
            Instance = this;
        }
    }

}