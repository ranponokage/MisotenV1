using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    [SerializeField] private float smooth = 60.0f;

    // Start is called before the first frame update
    void Start()
    {
        transform.DORotate(new Vector3(60, 170, 0), smooth).SetLoops(-1,LoopType.Yoyo);
    }

    // Update is called once per frame
    void Update()
    {
            ;
    }
}
