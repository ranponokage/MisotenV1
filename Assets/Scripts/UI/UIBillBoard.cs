using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class UIBillboard : MonoBehaviour
{
    [SerializeField] public Transform gameplayCameraTransform;
    // Start is called before the first frame update

    void OnDisable()
    {
        gameplayCameraTransform = null;
    }

    void LateUpdate()
    {

        if (gameplayCameraTransform)
        {
            transform.LookAt(transform.position + gameplayCameraTransform.rotation * Vector3.forward, gameplayCameraTransform.rotation * Vector3.up);
        }
    }
}
