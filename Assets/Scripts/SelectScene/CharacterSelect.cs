
using System;
using UnityEngine;
using TMPro;


public class CharacterSelect : MonoBehaviour
{
    private int _playerIndex;

    [SerializeField] private GameObject[] _characters;
    [SerializeField] private int _selectedCharacterIndex = 0;

    [SerializeField] private TMP_Text _playerNum;


    private float ignoreInputTime = 1.5f;
    private bool inputEnabled;

    private void Start()
    {

    }

    public void NextCharacter()
    {
        _characters[_selectedCharacterIndex].gameObject.SetActive(false);
        _selectedCharacterIndex = (_selectedCharacterIndex + 1) % _characters.Length;
        _characters[_selectedCharacterIndex].gameObject.SetActive(true);
    }
    public void PreviousCharacter()
    {
        _characters[_selectedCharacterIndex].gameObject.SetActive(false);
        _selectedCharacterIndex--;

        if (_selectedCharacterIndex < 0)
        {
            _selectedCharacterIndex += _characters.Length;
        }
        _characters[_selectedCharacterIndex].gameObject.SetActive(true);
    }

    internal void SetPlayerIndex(int playerIndex)
    {
        _playerIndex = playerIndex;
        _playerNum.SetText("Player " + (playerIndex + 1).ToString());
        ignoreInputTime = Time.time + ignoreInputTime;
    }

    void Update()
    {
        if (Time.time > ignoreInputTime)
        {
            inputEnabled = true;
        }
    }

    public void SetPlayerCharacterIndex()
    {
        if (!inputEnabled) { return; }

        PlayerConfigurationManager.Instance.SetPlayerCharacterIndex(_playerIndex, _selectedCharacterIndex);

        ES3.Save($"Player{_playerIndex + 1}.SelectedCharacter", _selectedCharacterIndex);
    }

    public void SetPlayerReady()
    {
        if (!inputEnabled) { return; }

        PlayerConfigurationManager.Instance.ReadyPlayer(_playerIndex);
    }

}
