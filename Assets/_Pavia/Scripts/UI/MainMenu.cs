using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Pavia.Core;

namespace Pavia.UI
{
    public class MainMenu : MonoBehaviour
    {

        [SerializeField] private Button _submitButton;
        [SerializeField] private TMP_InputField _usernameInput;
        [SerializeField] private TMP_InputField _avatarURLInput;
        [SerializeField] private TMP_InputField _pcaNameInput;

        private InitialPlayerFormData _playerData;

        private void Awake()
        {
            _playerData = PlayerDataManager.Instance.GetComponent<InitialPlayerFormData>();
        }

        public void OnUsernameEntered()
        {
            _playerData.PlayerUsername = _usernameInput.text;
        }

        public void OnAvatarURLEntered()
        {
            _playerData.PlayerAvatarURL = _avatarURLInput.text;
        }

        public void OnPcaNameEntered()
        {
            _playerData.PcaName = _pcaNameInput.text;
        }

        public void OnButtonSubmit()
        {
            SceneManager.LoadScene("Viewer");
        }

    }

}