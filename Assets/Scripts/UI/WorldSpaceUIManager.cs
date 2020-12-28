using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WorldSpaceUIManager : MonoBehaviour
{
    [SerializeField] TMP_Text _playerName;


    [SerializeField] TMP_Text _skillText;

    // Start is called before the first frame update
    void Start()
    {
        var index = GetComponent<Player>().GetPlayerIndex();

        _playerName.SetText("Player " + (index + 1));

        if (index == 0)
        {
            _playerName.color = Color.red;

        }
        else if (index == 1)
        {
            _playerName.color = Color.blue;
        }
    }

    public void ShowSkillText()
    {

    }

}
