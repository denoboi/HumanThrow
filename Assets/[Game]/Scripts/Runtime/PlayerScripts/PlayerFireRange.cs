using System;
using System.Collections;
using System.Collections.Generic;
using HCB.Core;
using UnityEngine;

public class PlayerFireRange : MonoBehaviour
{
    public static PlayerFireRange Instance;
    public float DestroyTime;

    private void Awake()
    {
        Instance = this;
    }


    private void OnEnable()
    {
       
        HCB.Core.EventManager.OnFireRangeGateInteracted.AddListener(IncreaseProjectileRange);
    }

    private void OnDisable()
    {
        if (Managers.Instance == null)
            return;
       
        HCB.Core.EventManager.OnFireRangeGateInteracted.RemoveListener(IncreaseProjectileRange);
    }



    private void IncreaseProjectileRange()
    {
        DestroyTime += .2f;
    }
}
