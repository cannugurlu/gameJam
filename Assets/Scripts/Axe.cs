using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Axe : MonoBehaviour
{
    [SerializeField] Vector3 endValue;
    [SerializeField] float playTime;
    void Start()
    {
        transform.DORotate(endValue,playTime).SetEase( Ease.InOutSine ).SetLoops(-1, LoopType.Yoyo);
    }

}
