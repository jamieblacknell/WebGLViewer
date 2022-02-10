using Pavia.Core;
using UnityEngine;
using UnityEngine.InputSystem;
using Wolf3D.ReadyPlayerMe.AvatarSDK;

namespace Pavia.Avatar
{
    public class AvatarLoadRuntime : MonoBehaviour
    {
        private const string INPUT_ASSET_PATH = "StarterAssets";
        private const string ANIMATOR_PATH = "StarterAssetsThirdPerson";


        private void Start()
        {
            AvatarLoader avatarLoader = new AvatarLoader();
            avatarLoader.LoadAvatar(PlayerDataManager.Instance.GetComponent<InitialPlayerFormData>().PlayerAvatarURL, OnAvavatarImported, OnAvatarLoaded);
        }

        private void OnAvatarLoaded(GameObject avatar, AvatarMetaData metaData)
        {
            Debug.Log("Avatar Loaded");
            SetupCharacter(avatar);
        }

        private void OnAvavatarImported(GameObject avatar)
        {
            Debug.Log("Avatar Imported");
        }

        private void SetupCharacter(GameObject avatar)
        {
            GameObject cameraTarget = new GameObject("Camera Target");
            avatar.tag = "Player";
            cameraTarget.transform.parent = avatar.transform;
            cameraTarget.transform.localPosition = new Vector3(0, 1.5f, 0);


            StarterAssets.ThirdPersonController tpsController = avatar.AddComponent<StarterAssets.ThirdPersonController>();
            tpsController.GroundedOffset = 0.1f;
            tpsController.GroundLayers = 1;
            tpsController.JumpTimeout = 0.5f;
            tpsController.CinemachineCameraTarget = cameraTarget;

            Animator animator = avatar.GetComponent<Animator>();
            animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(ANIMATOR_PATH);
            animator.applyRootMotion = false;

            CharacterController characterController = avatar.GetComponent<CharacterController>();
            characterController.center = new Vector3(0, 1, 0);
            characterController.radius = 0.3f;
            characterController.height = 1.9f;

            PlayerInput playerInput = avatar.GetComponent<PlayerInput>();
            playerInput.actions = Resources.Load<InputActionAsset>(INPUT_ASSET_PATH);
            playerInput.actions.Enable();

            avatar.AddComponent<BasicRigidBodyPush>();
            avatar.AddComponent<StarterAssets.StarterAssetsInputs>();

            Cinemachine.CinemachineVirtualCamera followCamera = GameObject.Find("PlayerFollowCamera").GetComponent<Cinemachine.CinemachineVirtualCamera>();
            followCamera.Follow = cameraTarget.transform;

        }
    } 
}
