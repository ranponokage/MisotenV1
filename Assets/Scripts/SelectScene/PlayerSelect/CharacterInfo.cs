using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfo : MonoBehaviour
{
    [SerializeField] string CharacterType;

    [HideInInspector]
    [SerializeField] string CharacterName;
    [SerializeField] string Description;
}
