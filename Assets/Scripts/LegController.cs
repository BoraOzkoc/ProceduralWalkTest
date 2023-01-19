using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegController : MonoBehaviour
{
    [SerializeField] private Transform target, hint;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float threshHold;
    [SerializeField] private int id;
    private Vector3 _targetPos, defaultLegPosition;
    private bool _legPlaced;
    private Coroutine _positionCoroutine;
    private SpiderController _spiderController;

    public void Init(SpiderController spiderController)
    {
        _spiderController = spiderController;
        _targetPos = target.localPosition;
        defaultLegPosition = transform.InverseTransformPoint(_targetPos);
    }

    private void FixedUpdate()
    {
        if (_legPlaced)
        {
            CheckLegDistance(RayCast());
        }
        else
        {
            _spiderController.JoinQueue(this);
        }

        target.position = _targetPos;
    }

    private void CheckLegDistance(Vector3 pos)
    {
        Debug.Log("distance checking");
        if (Vector3.Distance(pos, target.position) > threshHold)
        {
            Debug.Log("too far");

            _legPlaced = false;
        }
    }

    private Vector3 RayCast()
    {
        RaycastHit hitData;

        Ray ray = new Ray(hint.position, (transform.TransformPoint(defaultLegPosition) - hint.position).normalized);

        if (Physics.Raycast(ray, out hitData, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(ray.origin, ray.direction, Color.green);
            return hitData.point;
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * .75f, Color.red);

            return ray.GetPoint(.75f);
        }
    }

    public void PositionLeg()
    {
        IEnumerator PositionCoroutine()
        {
            Vector3 pos = RayCast();

            Vector3 risePoint = ((_targetPos + pos) / 2) + (Vector3.up * 0.05f);
            _targetPos = risePoint;
            _legPlaced = true;

            yield return new WaitForSeconds(0.05f);

            _targetPos = pos;
            Debug.Log("leg positioned");
        }

        // if (_positionCoroutine != null) StopCoroutine(_positionCoroutine);

        _positionCoroutine = StartCoroutine(PositionCoroutine());

        // if (Vector3.Distance(target.position, pos) > threshHold)
        // {
        //     target.position = pos;
        //     _targetPos = target.localPosition;
        // }
    }
}