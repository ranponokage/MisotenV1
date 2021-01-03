using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class SelectSceneSpawnPlayerSetup : MonoBehaviour
{
    [SerializeField] private GameObject _playerSetupPrefab;

    private GameObject _playerSetupRoot;
    private PlayerInput _playerInput;
    private CameraManager _cameraManager;
    private GameInput gameInput;

    void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        var index = _playerInput.playerIndex;


        _playerSetupRoot = GameObject.Find("PlayerSetupRoot");
        if (_playerSetupRoot != null)
        {
            var playerSetup = Instantiate(_playerSetupPrefab, _playerSetupRoot.transform);
            _playerInput.uiInputModule = playerSetup.GetComponentInChildren<InputSystemUIInputModule>();
            playerSetup.GetComponentInChildren<CharacterSelect>().SetPlayerIndex(_playerInput.playerIndex);

            _cameraManager = playerSetup.GetComponentInChildren<CameraManager>();
            _cameraManager.SetPlayerIndex(_playerInput.playerIndex);
        }

        _playerInput.onActionTriggered += _playerInput_onActionTriggered;

        gameInput = new GameInput();
    }

    private void _playerInput_onActionTriggered(InputAction.CallbackContext obj)
    {
        if (obj.action.name == gameInput.Gameplay.MouseControlCamera.name)
        {
            OnMouseControlCamera(obj);
        }
        if (obj.action.name == gameInput.Gameplay.RotateCamera.name)
        {
            OnRotateCamera(obj);
        }
    }


    public void OnMouseControlCamera(InputAction.CallbackContext context)
    {
        if (_cameraManager == null)
            return;
        if (context.phase == InputActionPhase.Performed)
            _cameraManager.OnEnableMouseControlCamera();

        if (context.phase == InputActionPhase.Canceled)
            _cameraManager.OnDisableMouseControlCamera();
    }

    public void OnRotateCamera(InputAction.CallbackContext context)
    {
        if (_cameraManager == null)
            return;
        _cameraManager.OnCameraMove(context.ReadValue<Vector2>(), IsDeviceMouse(context));
    }

    private bool IsDeviceMouse(InputAction.CallbackContext context)
    {
        return context.control.device.name == "Mouse";
    }

}
