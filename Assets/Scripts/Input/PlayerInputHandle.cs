using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.InputSystem;
using System.Linq;

public class PlayerInputHandle: MonoBehaviour
{
    private PlayerInput _playerInput;
    private Player _player;
    private bl_MiniMap _miniMap;
    private CameraManager _cameraManager;

    /*
    // Gameplay
    public event UnityAction accelerateEvent;
    public event UnityAction accelerateCanceledEvent;
    public event UnityAction attackEvent;  //skill 
    public event UnityAction interactEvent; // Used to talk, pickup objects, interact with tools like the cooking cauldron
    public event UnityAction extraActionEvent; // Used to bring up the inventory
    public event UnityAction pauseEvent;
    public event UnityAction<Vector2> moveEvent;
    public event UnityAction<Vector2, bool> cameraMoveEvent;
    public event UnityAction enableMouseControlCameraEvent;
    public event UnityAction disableMouseControlCameraEvent;

    public event UnityAction minimapToggleEvent;
    public event UnityAction minimapZoomInEvent;
    public event UnityAction minimapZoomOutEvent;

    // Dialogue
    public event UnityAction advanceDialogueEvent = delegate { };
    public event UnityAction onMoveSelectionEvent = delegate { };
    */


    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        var index = _playerInput.playerIndex;

        var players = FindObjectsOfType<Player>();
        _player = players.FirstOrDefault(p => p.GetPlayerIndex() == index);


        var miniMaps = FindObjectsOfType<bl_MiniMap>();
        _miniMap = miniMaps.FirstOrDefault(m => m.GetPlayerIndex() == index);

        var cameraManagers = FindObjectsOfType<CameraManager>();
        _cameraManager = cameraManagers.FirstOrDefault(c => c.GetPlayerIndex() == index);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (_player == null)
            return;
        _player.SetRawInput(context.ReadValue<Vector2>());
    }

    public void OnAccelerate(InputAction.CallbackContext context)
    {
        if (_player == null)
            return;
        if (context.phase == InputActionPhase.Performed)
            _player.IsAcceleratedPressed = true;
        if (context.phase == InputActionPhase.Canceled)
            _player.IsAcceleratedPressed = false;
    }


    public void OnAttack(InputAction.CallbackContext context)
    {
        if (_player == null)
            return;
        if (context.phase == InputActionPhase.Performed)
            _player.IsAttackedPressed = true;
        if (context.phase == InputActionPhase.Canceled)
            _player.IsAttackedPressed = false;
    }

    public void OnExtraAction(InputAction.CallbackContext context)
    {
        if (_player == null)
            return;
        if (context.phase == InputActionPhase.Performed)
            _player.IsExtraActionPressed = true;
        if (context.phase == InputActionPhase.Canceled)
            _player.IsExtraActionPressed = false;
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (_player == null)
            return;
        if (context.phase == InputActionPhase.Performed)
            _player.IsInteractionPressed = true;
        if (context.phase == InputActionPhase.Canceled)
            _player.IsInteractionPressed = false;
    }

    public void OnMinimapToggle(InputAction.CallbackContext context)
    {
        if (_miniMap == null)
            return;
        if (context.phase == InputActionPhase.Performed)
            _miniMap.OnMapToggle();
    }

    public void OnMinimapZoomIn(InputAction.CallbackContext context)
    {
        if (_miniMap == null)
            return;
        if (context.phase == InputActionPhase.Performed)
            _miniMap.OnMapZoomIn();
    }

    public void OnMinimapZoomOut(InputAction.CallbackContext context)
    {
        if (_miniMap == null)
            return;
        if (context.phase == InputActionPhase.Performed)
           _miniMap.OnMapZoomOut();
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
    //public void OnPause(InputAction.CallbackContext context)
    //{
    //    throw new System.NotImplementedException();
    //}
}
