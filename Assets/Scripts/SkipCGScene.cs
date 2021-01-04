using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Michsky.LSS;

public class SkipCGScene : MonoBehaviour
{
    Player _player;
    [SerializeField] LoadingScreenManager _lsm;

    private void Awake()
    {
        _player = ReInput.players.GetPlayer(0);
    }

    private void Update()
    {
        if(_player.GetAnyButton())
        {
            _lsm.LoadScene("PlayerSelect");
        }

    }
}
