using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Michsky.LSS;

public class CharacterSelectUIControl : MonoBehaviour
{
    [SerializeField] GameModeSO _gameModeSO;

    [SerializeField] private LoadingScreenManager _loadingScreenManager;

    [SerializeField] Button P1Submit;
    [SerializeField] Button P1Previous;
    [SerializeField] Button P1Next;
    [SerializeField] Button P2Submit;
    [SerializeField] Button P2Previous;
    [SerializeField] Button P2Next;

    private bool P1Submit_IsPressed;
    private bool P2Submit_IsPressed;
    private void Awake()
    {
       
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
                    //load Level1
                    _loadingScreenManager.LoadScene("Level1");
                }
                break;
            case GameMode.TwoPlayer:
                if (P1Submit_IsPressed && P2Submit_IsPressed)
                {
                    //load Level1
                    _loadingScreenManager.LoadScene("Level1");
                }
                break;
            default:
                break;
        }
    }
}
