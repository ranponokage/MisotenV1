using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    [SerializeField] private float smooth = 60.0f;

    private Tween _sunMove;

    // Start is called before the first frame update
    void Start()
    {
        _sunMove = transform.DORotate(new Vector3(60, 170, 0), smooth).SetLoops(-1,LoopType.Yoyo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        _sunMove.Kill();
    }
}
