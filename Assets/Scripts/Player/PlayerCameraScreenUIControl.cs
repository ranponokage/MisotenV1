using System.Collections;
using UnityEngine;


public class PlayerCameraScreenUIControl : MonoBehaviour
{
    [SerializeField] Canvas SkillCanvas;

    [HideInInspector] public Camera UICamera;

    private void Awake()
    {
        SkillCanvas.worldCamera = UICamera;
        SkillCanvas.planeDistance = 0.2f;
    }
}
