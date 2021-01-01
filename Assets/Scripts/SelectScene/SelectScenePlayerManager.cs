using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;
using UnityEngine.UI;

public class SelectScenePlayerManager : MonoBehaviour
{
    [SerializeField] GameObject _p2Selection;
    [SerializeField] Camera _p1Camera;
    [SerializeField] Camera _p2Camera;
    [SerializeField] CinemachineFreeLook _p1FollowCamera;
    [SerializeField] CinemachineFreeLook _p2FollowCamera;
    [SerializeField] Canvas _p2UI;

    // Start is called before the first frame update
    void Awake()
    {
        switch (GameManager.Instance.gameMode)
        {
            case GameMode.OnePlayer:
                SetOnePlayerMode();
                break;
            case GameMode.TwoPlayer:
                SetTwoPlayerMode();
                break;
            default:
                break;
        }
    }

    private void SetOnePlayerMode()
    {
        _p2Selection.SetActive(false);
        _p2UI.gameObject.SetActive(false);
        _p2Camera.gameObject.SetActive(false);
        _p2FollowCamera.gameObject.SetActive(false);
        _p1Camera.rect = new Rect(0, 0, 1.0f, 1.0f);
    }

    private void SetTwoPlayerMode()
    {
        _p2Selection.SetActive(true);
        _p2UI.gameObject.SetActive(true);
        _p2Camera.gameObject.SetActive(true);
        _p2FollowCamera.gameObject.SetActive(true);
        _p1Camera.rect = new Rect(0, 0.5f, 1.0f, 0.5f);
        _p2Camera.rect = new Rect(0, 0, 1.0f, 0.5f);

    }

    public void SetPlayerContextActive(int playerindex)
    {

    }
}
