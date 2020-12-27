using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.InputSystem;
using System.Linq;

public class InputHandle: MonoBehaviour
{
    private PlayerInput _playerInput;
    private Player _player;

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
    }

    public void OnMove(InputAction.CallbackContext context)
    {

        Debug.Log(context.ReadValue<Vector2>());
        //if (_player == null)
        //    return;
        //_player.SetRawInput(context.ReadValue<Vector2>());
    }

    public void OnAccelerate(InputAction.CallbackContext context)
    {
        //if (context.phase == InputActionPhase.Performed)
        //    _player.IsAccelerated = true;
        //if (context.phase == InputActionPhase.Canceled)
        //    _player.IsAccelerated = false;

        Debug.Log("_player.IsAccelerated = true;");
    }

    //public void OnAdvanceDIalogue(InputAction.CallbackContext context)
    //{
    //    throw new System.NotImplementedException();
    //}

    //public void OnAttack(InputAction.CallbackContext context)
    //{
    //    throw new System.NotImplementedException();
    //}

    //public void OnExtraAction(InputAction.CallbackContext context)
    //{
    //    throw new System.NotImplementedException();
    //}

    //public void OnInteract(InputAction.CallbackContext context)
    //{
    //    throw new System.NotImplementedException();
    //}

    //public void OnMinimapToggle(InputAction.CallbackContext context)
    //{
    //    throw new System.NotImplementedException();
    //}

    //public void OnMinimapZoomIn(InputAction.CallbackContext context)
    //{
    //    throw new System.NotImplementedException();
    //}

    //public void OnMinimapZoomOut(InputAction.CallbackContext context)
    //{
    //    throw new System.NotImplementedException();
    //}

    //public void OnMouseControlCamera(InputAction.CallbackContext context)
    //{
    //    throw new System.NotImplementedException();
    //}

    //public void OnMoveSelection(InputAction.CallbackContext context)
    //{
    //    throw new System.NotImplementedException();
    //}

    //public void OnPause(InputAction.CallbackContext context)
    //{
    //    throw new System.NotImplementedException();
    //}

    //public void OnRotateCamera(InputAction.CallbackContext context)
    //{
    //    throw new System.NotImplementedException();
    //}
}
