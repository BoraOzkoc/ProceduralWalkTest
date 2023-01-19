using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float threshHold;
    private Vector3 _targetPos;

    public void Init()
    {
        _targetPos = target.localPosition;
    }

    private void FixedUpdate()
    {
        //CheckLegDistance();
    }

    private void CheckLegDistance()
    {
        Vector3 pos = transform.TransformPoint(_targetPos);

        if (Vector3.Distance(target.position,pos) > threshHold)
        {
            target.position = pos;
        }
    }
}
