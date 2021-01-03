using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerConfigurationManager : Singleton<PlayerConfigurationManager>
{
    [SerializeField] private GameModeSO _gameModeSO;
    private List<PlayerConfiguration> _playerConfigs;
    private PlayerInputManager _playerInputManager;

    [SerializeField] public int _maxPlayers = 2;

    private void Awake()
    {
        _playerConfigs = new List<PlayerConfiguration>();

        switch (_gameModeSO.gameMode)
        {
            case GameMode.OnePlayer:
                _maxPlayers = 1;
                break;
            case GameMode.TwoPlayer:
                _maxPlayers = 2;
                break;
            default:
                break;
        }    
    }

    public void SetPlayerCharacterIndex(int index, int charactertIndex)
    {
        _playerConfigs[index].CharactertIndex = charactertIndex;
    }


    public void ReadyPlayer(int index)
    {
        _playerConfigs[index].IsReady = true;
        if (_playerConfigs.Count == _maxPlayers && _playerConfigs.All(p=>p.IsReady == true))
        {
            //Start Game
        }
    }
    public List<PlayerConfiguration> GetPlayerConfigs()
    {
        return _playerConfigs;
    }

    public void HandlePlayerJoin(PlayerInput playerInput)
    {
        Debug.Log("Player Joined　" + playerInput.playerIndex);
        if(!_playerConfigs.Any(p=>p.PlayerIndex == playerInput.playerIndex))
        {
            playerInput.transform.SetParent(transform);
            _playerConfigs.Add(new PlayerConfiguration(playerInput));
        }
    }
}

public class PlayerConfiguration
{
    public PlayerConfiguration (PlayerInput playerinput)
    {
        Input = playerinput;
        PlayerIndex = playerinput.playerIndex;
    }
    public PlayerInput Input { get; set; }
    public int PlayerIndex {get; set;}
    public bool IsReady { get; set; }
    public int CharactertIndex { get; set; }
}