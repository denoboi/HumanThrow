using System;
using System.Collections;
using System.Collections.Generic;
using HCB.Core;
using HCB.IncrimantalIdleSystem;
using UnityEngine;

public class PlayerFireRange : IdleStatObjectBase
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

    public override void UpdateStat(string id)
    {
        throw new NotImplementedException();
    }


    private void IncreaseProjectileRange()
    {
        DestroyTime = (float)IdleStat.CurrentValue;
    }
}
