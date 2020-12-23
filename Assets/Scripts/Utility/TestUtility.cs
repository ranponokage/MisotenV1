using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class TestUtility : MonoBehaviour
{
    void Update()
    {
        var keyboard = Keyboard.current; 

        if (keyboard.rKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
