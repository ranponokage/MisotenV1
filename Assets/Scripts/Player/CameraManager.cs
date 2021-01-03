using Cinemachine;

using UnityEngine;
using Rewired;
using System;

public class CameraManager : MonoBehaviour
{
    public int PlayerIndex;
    public Camera PlayerCamera;
    public CinemachineFreeLook FreeLookVCam;
    private bool _isRMBPressed;

    private Vector2 _lookVector;
    private Rewired.Player _rPlayer;

    [SerializeField, Range(1f, 5f)]
    private float speed = 2f;

    private void Awake()
    {
        _rPlayer = ReInput.players.GetPlayer(PlayerIndex);
    }

    private void Update()
    {
        //GetLookInput();
        //if(_rPlayer.GetButton("MouseControlCamera"))
        //{
        //    EnableMouseControlCamera();
        //}
        //else
        //{
        //    DisableMouseControlCamera();

        //}
        // OnCameraMove(_lookVector);
    }

    private void GetLookInput()
    {
        _lookVector.x = _rPlayer.GetAxis("Look X"); // get input by name or action id
        _lookVector.y = _rPlayer.GetAxis("Look Y");
    }

    public int GetPlayerIndex() { return PlayerIndex; }

    public void SetupPlayerVirtualCamera(Transform target)
    {
        FreeLookVCam.Follow = target;
        FreeLookVCam.LookAt = target;
    }

    private void EnableMouseControlCamera()
    {
        _isRMBPressed = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void DisableMouseControlCamera()
    {
        _isRMBPressed = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // when mouse control is disabled, the input needs to be cleared
        // or the last frame's input will 'stick' until the action is invoked again
        FreeLookVCam.m_XAxis.m_InputAxisValue = 0;
        FreeLookVCam.m_YAxis.m_InputAxisValue = 0;
    }

    public void OnCameraMove(Vector2 cameraMovement)
    {

        if (!_isRMBPressed)
            return;
        FreeLookVCam.m_XAxis.m_InputAxisValue = cameraMovement.x * Time.smoothDeltaTime * speed;
        FreeLookVCam.m_YAxis.m_InputAxisValue = cameraMovement.y * Time.smoothDeltaTime * speed;
    }
}
