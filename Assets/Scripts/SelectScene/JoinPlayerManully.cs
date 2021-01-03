using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class JoinPlayerManully : Singleton<JoinPlayerManully>
{
    [SerializeField] private GameModeSO _gameModeSO;
    [SerializeField] private PlayerInput _playerInputPrefab;
    [SerializeField] private InputSystemUIInputModule _uIInputModule_P1;
    [SerializeField] private InputSystemUIInputModule _uIInputModule_P2;

    [SerializeField] private PlayerInputManager _playerInputManager;

    private PlayerInput player1;
    private PlayerInput player2;

    private void Awake()
    {
        switch (_gameModeSO.gameMode)
        {
            case GameMode.OnePlayer:
                player1 = Instantiate(_playerInputPrefab, transform);
                player1.uiInputModule = _uIInputModule_P1;
                _playerInputManager.JoinPlayer(0);
                break;
            case GameMode.TwoPlayer:
                player1 = Instantiate(_playerInputPrefab, transform);
                player1.uiInputModule = _uIInputModule_P1;
                _playerInputManager.JoinPlayer(0, -1, "Gamepad");
                player2 = Instantiate(_playerInputPrefab, transform);
                player2.uiInputModule = _uIInputModule_P2;
                _playerInputManager.JoinPlayer(1, -1, "Gamepad");
                break;
            default:
                break;
        }
        _playerInputManager.onPlayerJoined += _playerInputManager_onPlayerJoined;

    }

    private void _playerInputManager_onPlayerJoined(PlayerInput playerInput)
    {
        DontDestroyOnLoad(playerInput.gameObject);
    }
}
