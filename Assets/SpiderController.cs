using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderController : MonoBehaviour
{
    [SerializeField] private List<LegController> legList = new List<LegController>();

    
    public void Init()
    {
        for (int i = 0; i < legList.Count; i++)
        {
            legList[i].Init();

        }
    }


    
}
