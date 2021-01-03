using System.Collections;
using UnityEngine;


public class PlayerCharacterGroup : MonoBehaviour
{
    [SerializeField] private int _playerIndex = -1;

    [SerializeField] private PlayerControl[] _characterPrefabs;
    
    public PlayerControl GetCharacter(int index)
    {
        if (index < _characterPrefabs.Length)
        {
            return _characterPrefabs[index];
        }
        else
        {
            Debug.Log("No Character in this index");
            return null;
        }
    }
    public int GetPlayerIndex() { return _playerIndex; }
}
