using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetupMenuController: MonoBehaviour
{
    private int _playerIndex;
    private float ignoreInputTime = 1.5f;
    private bool inputEnabled;

    public void SetPlayerIndex (int playerindex)
    {
        _playerIndex = playerindex;
        ignoreInputTime = Time.time + ignoreInputTime;
    }

    public void SetPlayerNameColor(Color color)
    {
        if (!inputEnabled) { return; }

        PlayerConfigurationManager.Instance.SetPlayerNameColor(_playerIndex, color);
    }

    public void SetPlayerCharacterIndex(int index)
    {
        if(!inputEnabled) { return; }

        PlayerConfigurationManager.Instance.SetPlayerCharacterIndex(_playerIndex, index);
    }

    public void SetPlayerReady()
    {
        if (!inputEnabled) { return; }

        PlayerConfigurationManager.Instance.ReadyPlayer(_playerIndex);
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > ignoreInputTime)
        {
            inputEnabled = true;
        }
    }
}
