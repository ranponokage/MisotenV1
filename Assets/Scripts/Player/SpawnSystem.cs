﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    [SerializeField] PlayerCharacterGroup _playerCharacterGroup;
    [SerializeField] CameraManager[] _cameraManager;
    [SerializeField] GameObject[] _minimap;
    [SerializeField] private Transform[] _spawnLocations;

    [SerializeField] private GameModeSO gameModeSO;

    [SerializeField] StatusPanel[] _statsPanels;


    private int[] _playerCharacterIndex;
    private int _numberOfPlayers;

    // Start is called before the first frame update
    void Awake()
    {
        _playerCharacterIndex = new int[2];

        //_playerInputManager = FindObjectOfType<PlayerInputManager>();
        //_playerInputManager.playerPrefab = playerInputHandlePrefab;

        switch (gameModeSO.gameMode)
        {
            case GameMode.OnePlayer:
                //_playerCharacterIndex[0] = PlayerPrefs.GetInt("Player1.SelectedCharacter");
                _playerCharacterIndex[0] = ES3.Load<int>("Player1.SelectedCharacter");
                _numberOfPlayers = 1;
                SetupOnePlayerMode();
                break;
            case GameMode.TwoPlayer:
                //_playerCharacterIndex[1] = PlayerPrefs.GetInt("Player2.SelectedCharacter");
                _playerCharacterIndex[1] = ES3.Load<int>("Player2.SelectedCharacter");
                _numberOfPlayers = 2;
                SetupTwoPlayerMode();
                break;
            default:
                break;
        }
    }
    private void SetupOnePlayerMode()
    {
        SpawnPlayers(_playerCharacterIndex);
        SetupSinglePlayerCamera();
        SetupSinglePlayerMiniMap();
    }
    private void SetupTwoPlayerMode()
    {
        SpawnPlayers(_playerCharacterIndex);
        SetupTwoPlayerCamera();
        SetupTwoPlayerMiniMap();
    }
    private void SetupSinglePlayerCamera()
    {
        _cameraManager[0].gameObject.SetActive(true);
        _cameraManager[0].PlayerCamera.rect = new Rect(0f, 0f, 1f, 1f);
        _cameraManager[1].gameObject.SetActive(false);
    }
    private void SetupTwoPlayerCamera()
    {
        //P1
        _cameraManager[0].gameObject.SetActive(true);
        _cameraManager[0].PlayerCamera.rect = new Rect(0f, 0.5f, 1f, 0.5f);
        //P2
        _cameraManager[1].gameObject.SetActive(true);
        _cameraManager[1].PlayerCamera.rect = new Rect(0f, 0f, 1f, 0.5f);
    }
    private void SpawnPlayers(int[] playerCharacterIndex)
    {
        for (int i = 0; i < _numberOfPlayers; i++)
        {
            var playerInstance = InstantiatePlayer(_playerCharacterGroup.GetCharacter(playerCharacterIndex[i]),
                _spawnLocations[i]);
            playerInstance.PlayerIndex = i;
            SetupMinimap(playerInstance, i);
            SetupMainCameras(playerInstance, _cameraManager[i]);
            SetupPlayerUICamera(playerInstance, _cameraManager[i]);
            //setup player StatsDisplayer
            playerInstance._statusPanel = _statsPanels[i];
        }
    }
    private void SetupSinglePlayerMiniMap()
    {
        _minimap[0].SetActive(true);
        _minimap[1].SetActive(false);

    }
    private void SetupTwoPlayerMiniMap()
    {
        _minimap[0].SetActive(true);
        _minimap[1].SetActive(true);
    }
    private void SetupPlayerUICamera(PlayerControl playerInstance, CameraManager cameraManager)
    {
        var uibillboard = playerInstance.GetComponentInChildren<UIBillboard>();
        uibillboard.gameplayCameraTransform = cameraManager.PlayerCamera.transform;
        //var canvasControl = playerInstance.GetComponentInChildren<PlayerCameraScreenUIControl>();
        //canvasControl.UICamera = cameraManager.PlayerCamera;
    }
    private void SetupMainCameras(PlayerControl playerInstance, CameraManager cameraManager)
    {
        playerInstance.GameplayCamera = cameraManager.PlayerCamera;
        cameraManager.SetupPlayerVirtualCamera(playerInstance.transform);
        playerInstance.FreeLookVCam = cameraManager.FreeLookVCam;
    }
    private void SetupMinimap(PlayerControl playerInstance, int playerIndex)
    {
        _minimap[playerIndex].GetComponentInChildren<bl_MiniMap>().PlayerIndex = playerIndex;
        _minimap[playerIndex].GetComponentInChildren<bl_MiniMap>().SetTarget(playerInstance.gameObject);
    }
    private PlayerControl InstantiatePlayer(PlayerControl playerPrefab, Transform spawnLocation)
    {
        if (playerPrefab == null)
            throw new Exception("Player Prefab can't be null.");

        PlayerControl playerInstance = Instantiate(playerPrefab, spawnLocation.position, spawnLocation.rotation);

        return playerInstance;
    }

}
