using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateEffect : MonoBehaviour
{
    [SerializeField] private Vector3 _rotateAngle = new Vector3(0,1,0);
    [SerializeField] private float _repeatRate;

    private void Start()
    {
//        InvokeRepeating("Rotate", 0, _repeatRate);
        StartCoroutine(RotateRoutine());
    }

    private IEnumerator RotateRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_repeatRate);
            transform.Rotate(_rotateAngle, Space.Self);
        }
    }

    private void Rotate()
    {
        transform.Rotate(_rotateAngle, Space.Self);
    }
}
