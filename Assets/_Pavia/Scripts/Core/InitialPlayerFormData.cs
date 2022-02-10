using UnityEngine;

namespace Pavia.Core
{

    public class InitialPlayerFormData : MonoBehaviour
    {

        private const string DEFAULT_USERNAME = "Player";
        private const string DEFAULT_AVATAR_URL = "https://d1a370nemizbjq.cloudfront.net/b36a3004-4916-481c-b31a-c3e8f9c86466.glb";
        private const string DEFAULT_PCA_NAME = "example";

        public string PlayerUsername { get; set; } = DEFAULT_USERNAME;
        public string PlayerAvatarURL { get; set; } = DEFAULT_AVATAR_URL;
        public string PcaName { get; set; } = DEFAULT_PCA_NAME;

    } 
}
