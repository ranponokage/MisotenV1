
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    [SerializeField] GameObject[] characters; 
    [SerializeField] int selectedCharacter = 0;

    public void NextCharacter()
    {
        characters[selectedCharacter].gameObject.SetActive(false);
        selectedCharacter = (selectedCharacter + 1) % characters.Length;
        characters[selectedCharacter].gameObject.SetActive(true);
    }

    public void PreviousCharacter()
    {
        characters[selectedCharacter].gameObject.SetActive(false);
        selectedCharacter--;

        if (selectedCharacter < 0)
        {
            selectedCharacter += characters.Length;
        }
        characters[selectedCharacter].gameObject.SetActive(true);
    }

    public void SetPlayer1()
    {
        //PlayerPrefs.SetInt("Player1.SelectedCharacter", selectedCharacter);
        ES3.Save("Player1.SelectedCharacter", selectedCharacter);
        // LOAD SCENE

    }
    public void SetPlayer2()
    {
        //PlayerPrefs.SetInt("Player2.SelectedCharacter", selectedCharacter);
        ES3.Save("Player2.SelectedCharacter", selectedCharacter);
        // LOAD SCENE

    }
}
