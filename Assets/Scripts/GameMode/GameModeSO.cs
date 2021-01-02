using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameMode
{
    OnePlayer,
    TwoPlayer,
}

[CreateAssetMenu(menuName = "GameModeSO")]
public class GameModeSO : ScriptableObject
{
    public GameMode gameMode;

    public void SetGameMode(int mode)
    {
        gameMode = (GameMode)mode;
    }
}
