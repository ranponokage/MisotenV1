
using UnityEngine;
using UnityEngine.UI;


public class CharacterSelect : MonoBehaviour
{
    [SerializeField] GameObject[] _characters;
    [SerializeField] int _selectedCharacterIndex = 0;



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


    public void SetPlayer1()
    {
        //PlayerPrefs.SetInt("Player1.SelectedCharacter", selectedCharacter);
        ES3.Save("Player1.SelectedCharacter", _selectedCharacterIndex);
        // LOAD SCENE

    }
    public void SetPlayer2()
    {
        //PlayerPrefs.SetInt("Player2.SelectedCharacter", selectedCharacter);
        ES3.Save("Player2.SelectedCharacter", _selectedCharacterIndex);
        // LOAD SCENE

    }
}
