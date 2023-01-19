using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpiderController : MonoBehaviour
{
    [SerializeField] private List<LegController> legList = new List<LegController>();
    [SerializeField] private Queue<LegController> legQueue = new Queue<LegController>();

    private Coroutine _queueCoroutine;

    public void Init()
    {
        for (int i = 0; i < legList.Count; i++)
        {
            legList[i].Init(this);
        }
    }


    public void JoinQueue(LegController legController)
    {
        if (!legQueue.Contains(legController))
        {
            legQueue.Enqueue(legController);

            Dequeue();
        }
    }

    private void Dequeue()
    {
        IEnumerator DequeueCoroutine()
        {
            while (legQueue.Count > 0)
            {
                legQueue.Dequeue().PositionLeg();

                yield return new WaitForSeconds(0.01f);
            }

            if (_queueCoroutine != null) _queueCoroutine = null;
        }

        if (_queueCoroutine == null) _queueCoroutine = StartCoroutine(DequeueCoroutine());
    }
}