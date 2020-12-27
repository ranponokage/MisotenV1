using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerConfigurationManager : Singleton<PlayerConfigurationManager>
{
    private List<PlayerConfiguration> _playerConfigs;

    [SerializeField] private int _maxPlayers = 2;

    private void Awake()
    {
        _playerConfigs = new List<PlayerConfiguration>();
    }

    public void SetPlayerNameColor(int index, Color color)
    {
        _playerConfigs[index].PlayerNameColor = color;
    }
    public void ReadyPlayer(int index)
    {
        _playerConfigs[index].IsReady = true;
        if (_playerConfigs.Count == _maxPlayers && _playerConfigs.All(p=>p.IsReady == true))
        {
            //Start Game
        }
    }

    public void HandlePlayerJoin(PlayerInput playerInput)
    {
        Debug.Log("Player Joined" + playerInput.playerIndex);
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

    public Color PlayerNameColor { get; set; }

}