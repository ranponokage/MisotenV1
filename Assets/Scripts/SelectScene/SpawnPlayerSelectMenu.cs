using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;


public class SpawnPlayerSelectMenu : MonoBehaviour
{
    public GameObject PlayerSelectUIPrefab;
    public PlayerInput _PlayerInput;
    // Start is called before the first frame update
    void Awake()
    {
        var uiRoot = GameObject.Find("UIRoot");
        if(uiRoot != null)
        {
            var menu = Instantiate(PlayerSelectUIPrefab, uiRoot.transform);
            _PlayerInput.uiInputModule = PlayerSelectUIPrefab.GetComponentInChildren<InputSystemUIInputModule>();
            menu.GetComponent<PlayerSetupMenuController>().SetPlayerIndex(_PlayerInput.playerIndex);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
