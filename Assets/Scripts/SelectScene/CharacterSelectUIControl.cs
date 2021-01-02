using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterSelectUIControl : MonoBehaviour
{
    [SerializeField] Button P1Submit;
    [SerializeField] Button P1Previous;
    [SerializeField] Button P1Next;
    [SerializeField] Button P2Submit;
    [SerializeField] Button P2Previous;
    [SerializeField] Button P2Next;

    [SerializeField] GameModeSO _gameModeSO;

    private StartGame startGame;
    private bool P1Submit_IsPressed;
    private bool P2Submit_IsPressed;
    private void Awake()
    {
        startGame = GetComponent<StartGame>();
    }

    private void Update()
    {
        StartGame();
    }
    public void OnP1SubmitPressed()
    {
        P1Previous.gameObject.SetActive(false);
        P1Next.gameObject.SetActive(false);
        P1Submit_IsPressed = true;
    }

    public void OnP2SubmitPressed()
    {
        P2Previous.gameObject.SetActive(false);
        P2Next.gameObject.SetActive(false);
        P2Submit_IsPressed = true;
    }

    public void StartGame()
    {
        switch (_gameModeSO.gameMode)
        {
            case GameMode.OnePlayer:
                if (P1Submit_IsPressed)
                {
                    startGame.OnPlayButtonPress();
                }
                break;
            case GameMode.TwoPlayer:
                if (P1Submit_IsPressed && P2Submit_IsPressed)
                {
                    startGame.OnPlayButtonPress();
                }
                break;
            default:
                break;
        }
    }
}
