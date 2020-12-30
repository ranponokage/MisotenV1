using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    [SerializeField] private float smooth = 5.0f;
    private Quaternion target;

    // Start is called before the first frame update
    void Start()
    {
         target = Quaternion.Euler(180, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(Time.realtimeSinceStartup,0,0));
    }
}
