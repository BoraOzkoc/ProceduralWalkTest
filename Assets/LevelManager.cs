using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private SpiderController spiderController;

    private void Start()
    {
        spiderController.Init();
        
    }
}
