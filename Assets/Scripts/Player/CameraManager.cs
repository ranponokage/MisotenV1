using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
   [SerializeField] GameModeSO _gameModeSO;
    private int PlayerIndex;
    public Camera PlayerCamera;
    public CinemachineFreeLook FreeLookVCam;
    private bool _isRMBPressed;


    [SerializeField, Range(1f, 5f)]
    private float speed = 2f;

    private void Awake()
    {
        SetupCameraRect();
    }

    private void SetupCameraRect()
    {
        switch (_gameModeSO.gameMode)
        {
            case GameMode.OnePlayer:
                PlayerCamera.rect = new Rect(0, 0, 1, 1);
                break;
            case GameMode.TwoPlayer:
                if (PlayerIndex == 0)
                {
                    PlayerCamera.rect = new Rect(0, 0.5f, 1.0f, 0.5f);
                }
                if (PlayerIndex == 1)
                {
                    PlayerCamera.rect = new Rect(0, 0f, 1.0f, 0.5f);
                }
                break;
            default:
                break;
        }
    }

    public int GetPlayerIndex() { return PlayerIndex; }

    public void SetupPlayerVirtualCamera(Transform target)
    {
        FreeLookVCam.Follow = target;
        FreeLookVCam.LookAt = target;
    }

    public void OnEnableMouseControlCamera()
    {
        _isRMBPressed = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void OnDisableMouseControlCamera()
    {
        _isRMBPressed = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // when mouse control is disabled, the input needs to be cleared
        // or the last frame's input will 'stick' until the action is invoked again
        FreeLookVCam.m_XAxis.m_InputAxisValue = 0;
        FreeLookVCam.m_YAxis.m_InputAxisValue = 0;
    }

    public void SetPlayerIndex(int playerIndex)
    {
        PlayerIndex = playerIndex;
        //SetupCameraRect();
    }

    public void OnCameraMove(Vector2 cameraMovement, bool isDeviceMouse)
    {

        if (isDeviceMouse && !_isRMBPressed)
            return;
        FreeLookVCam.m_XAxis.m_InputAxisValue = cameraMovement.x * Time.smoothDeltaTime * speed;
        FreeLookVCam.m_YAxis.m_InputAxisValue = cameraMovement.y * Time.smoothDeltaTime * speed;
    }
}
