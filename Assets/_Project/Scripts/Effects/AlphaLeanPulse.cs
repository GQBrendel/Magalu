using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaLeanPulse : MonoBehaviour
{
    [SerializeField] private Vector2 _alphaRange;
    [SerializeField] private float _repeatRate;

    private void Start()
    {
        LeanToMax();
    }

    private void LeanToMax()
    {
        LeanTween.alpha(gameObject, _alphaRange.y, _repeatRate).setOnComplete(LeanToMin);
    }
    private void LeanToMin()
    {
        LeanTween.alpha(gameObject, _alphaRange.x, _repeatRate).setOnComplete(LeanToMax);
    }
}
