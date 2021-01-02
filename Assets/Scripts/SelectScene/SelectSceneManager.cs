using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;
using UnityEngine.UI;

public class SelectSceneManager : MonoBehaviour
{
    [SerializeField] CameraManager[] _cameraManager;

    [SerializeField] Canvas _p2UI;
    [SerializeField] GameObject[] _characterSelection;


    [SerializeField] GameModeSO _gameModeSO;

    private int _numberOfPlayers;

    // Start is called before the first frame update
    void Awake()
    {
        switch (_gameModeSO.gameMode)
        {
            case GameMode.OnePlayer:
                _numberOfPlayers = 1;
                SetOnePlayerMode();
                break;
            case GameMode.TwoPlayer:
                _numberOfPlayers = 2;
                SetTwoPlayerMode();
                break;
            default:
                break;
        }

    }

    private void SetOnePlayerMode()
    {
        _cameraManager[0].PlayerCamera.rect = new Rect(0, 0, 1.0f, 1.0f);
        _cameraManager[0].SetupPlayerVirtualCamera(_characterSelection[0].transform);

        _p2UI.gameObject.SetActive(false);
        _characterSelection[1].SetActive(false);
        _cameraManager[1].gameObject.SetActive(false);

    }

    private void SetTwoPlayerMode()
    {
        _cameraManager[0].PlayerCamera.rect = new Rect(0, 0.5f, 1.0f, 0.5f);
        _cameraManager[0].SetupPlayerVirtualCamera(_characterSelection[0].transform);

        _p2UI.gameObject.SetActive(true);
        _characterSelection[1].SetActive(true);
        _cameraManager[1].gameObject.SetActive(true);
        _cameraManager[1].PlayerCamera.rect = new Rect(0, 0, 1.0f, 0.5f);
        _cameraManager[1].SetupPlayerVirtualCamera(_characterSelection[1].transform);

    }

    public void SetPlayerContextActive(int playerindex)
    {

    }
}
